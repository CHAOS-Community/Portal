using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using Amazon.SimpleEmail.Model;
using Chaos.Portal.Core.EmailService;

namespace Chaos.Portal.EmailService
{
	public class EmailService : IEmailService
	{
		private const uint MAX_RECIPIENTS = 50;

		private readonly IEmailSender _sender;

		public EmailService(IEmailSender sender)
		{
			_sender = sender;
		}

		#region Send

		public void Send(string from, string to, string subject, string body)
		{
			Send(from, new List<string> { to }, subject, body);
		}

		public void SendTemplate(string from, string to, string subject, XElement bodyTemplate, XElement bodyData)
		{
			SendTemplate(from, new List<string> { to }, subject, bodyTemplate, bodyData);
		}

		public void SendTemplate(string from, IEnumerable<string> to, string subject, XElement bodyTemplate, XElement bodyData)
		{
			Send(from, to, subject, TransformXSLT(bodyTemplate, bodyData));
		}

		public void SendTemplate(string @from, string to, string subject, XElement bodyTemplate, IList<XElement> bodyDatas)
		{
			SendTemplate(from, new List<string> { to }, subject, bodyTemplate, bodyDatas);
		}

		public void SendTemplate(string @from, IEnumerable<string> to, string subject, XElement bodyTemplate, IList<XElement> bodyDatas)
		{
			var root = new XElement("Root");

			foreach (var bodyData in bodyDatas)
				root.Add(bodyData);

			SendTemplate(from, to, subject, bodyTemplate, root);
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

				destinations.Add(new Destination { BccAddresses = currentBatch });
				currentBatch = new List<string>();
			}

			if (currentBatch.Count != 0)
				destinations.Add(new Destination { BccAddresses = currentBatch });

			foreach (var destination in destinations)
				_sender.Send(new SendEmailRequest(from, destination, message));
		}

		#endregion
		#region TransformXSLT

		private string TransformXSLT(XElement template, XElement data)
		{
			using (var templateXmlReader = template.CreateReader())
			using (var dataXmlReader = data.CreateReader())
			{
				var xslt = new XslCompiledTransform();
				xslt.Load(templateXmlReader);
				using (var stringWriter = new StringWriter())
				using (var xmlWriter = XmlWriter.Create(stringWriter, xslt.OutputSettings))
				{
					xslt.Transform(dataXmlReader, xmlWriter);
					return stringWriter.ToString();
				}
			}
		}
		
		#endregion
	}
}