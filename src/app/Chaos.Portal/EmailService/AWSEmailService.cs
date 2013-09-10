using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Chaos.Portal.Core.EmailService;

namespace Chaos.Portal.EmailService
{
	public class AWSEmailService : IEmailService
	{
		private readonly AmazonSimpleEmailServiceClient _client;
		private const uint MAX_RECIPIENTS = 50;

		public AWSEmailService(string accessKey, string secretKey)
		{
			_client = new AmazonSimpleEmailServiceClient(accessKey, secretKey);
		}

		public AWSEmailService(AmazonSimpleEmailServiceClient client)
		{
			_client = client;
		}

		#region Send

		public void Send(string from, string to, string subject, string body)
		{
			Send(from, new List<string> { to }, subject, body);
		}

		public void SendWithXSLT(string from, string to, string subject, string bodyTemplate, string bodyData)
		{
			SendWithXSLT(from, new List<string> { to }, subject, bodyTemplate, bodyData);
		}

		public void SendWithXSLT(string from, IEnumerable<string> to, string subject, string bodyTemplate, string bodyData)
		{
			Send(from, to, subject, TransformXSLT(bodyTemplate, bodyData));
		}

		public void Send(string from, IEnumerable<string> to, string subject, string body)
		{
			var bodyContent = new Content(body)
			{
				Charset = "UTF-8"
			};

			var message = new Message(new Content(subject), new Body { Html = bodyContent });

			var destinations = new List<Destination>();
			var currentBatch = new List<string>();

			foreach (var email in to)
			{
				currentBatch.Add(email);

				if (currentBatch.Count != MAX_RECIPIENTS) continue;

				destinations.Add(new Destination() { BccAddresses = currentBatch });
				currentBatch = new List<string>();
			}

			if (currentBatch.Count != 0)
				destinations.Add(new Destination() { BccAddresses = currentBatch });

			foreach (var destination in destinations)
				_client.SendEmail(new SendEmailRequest(from, destination, message));
		}

		#endregion
		#region TransformXSLT

		private string TransformXSLT(string template, string data)
		{
			using (var templateStringReader = new StringReader(template))
			using (var dataStringReader = new StringReader(data))
			{
				using (var templateXmlReader = XmlReader.Create(templateStringReader))
				using (var dataXmlReader = XmlReader.Create(dataStringReader))
				{
					var xslt = new XslCompiledTransform();
					xslt.OutputSettings.Encoding = Encoding.UTF8;
					xslt.Load(templateXmlReader);
					using (var stringWriter = new StringWriter())
					using (var xmlWriter = XmlWriter.Create(stringWriter, xslt.OutputSettings))
					{
						xslt.Transform(dataXmlReader, xmlWriter);
						return stringWriter.ToString();
					}
				}
			}
		}
		
		#endregion
	}
}