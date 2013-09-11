using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using Amazon.SimpleEmail.Model;
using Chaos.Portal.Core.EmailService;

namespace Chaos.Portal.EmailService
{
	public class EmailService : IEmailService
	{
		private readonly IEmailSender _sender;

		public EmailService(IEmailSender sender)
		{
			_sender = sender;
		}

		#region Send

		public void Send(string from, string to, string subject, string body)
		{
			Send(from, new List<string> { to }, null, subject, body);
		}

		public void Send(string from, IEnumerable<string> to, IEnumerable<string> bcc, string subject, string body)
		{
			var bodyContent = new Content(body)
			{
				Charset = "UTF-8"
			};

			var message = new Message(new Content(subject), new Body { Html = bodyContent });

			var destinations = new List<Destination>();
			var destination = new Destination();

			if (to != null)
			{
				foreach (var email in to)
				{
					if(string.IsNullOrEmpty(email))
						throw new ArgumentException("To Email can not be null or empty");

					destination.ToAddresses.Add(email);

					if (destination.ToAddresses.Count != _sender.MaxRecipientPerBatch) continue;

					destinations.Add(destination);
					destination = new Destination();
				}
			}

			if (bcc != null)
			{
				foreach (var email in bcc)
				{
					if (string.IsNullOrEmpty(email))
						throw new ArgumentException("Bbc Email can not be null or empty");

					destination.BccAddresses.Add(email);

					if (destination.ToAddresses.Count + destination.BccAddresses.Count != _sender.MaxRecipientPerBatch) continue;

					destinations.Add(destination);
					destination = new Destination();
				}
			}

			if (destination.ToAddresses.Count + destination.BccAddresses.Count != 0)
				destinations.Add(destination);

			foreach (var d in destinations)
				_sender.Send(new SendEmailRequest(from, d, message));
		}

		#endregion
		#region SendTemplate

		public void SendTemplate(string from, string to, string subject, XElement bodyTemplate, XElement bodyData)
		{
			SendTemplate(from, new List<string> { to }, null, subject, bodyTemplate, bodyData);
		}

		public void SendTemplate(string from, IEnumerable<string> to, IEnumerable<string> bcc, string subject, XElement bodyTemplate, XElement bodyData)
		{
			Send(from, to, bcc, subject, TransformXSLT(bodyTemplate, bodyData));
		}

		public void SendTemplate(string @from, string to, string subject, XElement bodyTemplate, IList<XElement> bodyDatas)
		{
			SendTemplate(from, new List<string> { to }, null, subject, bodyTemplate, bodyDatas);
		}

		public void SendTemplate(string @from, IEnumerable<string> to, IEnumerable<string> bcc, string subject, XElement bodyTemplate, IList<XElement> bodyDatas)
		{
			var root = new XElement("Root");

			foreach (var bodyData in bodyDatas)
				root.Add(bodyData);

			SendTemplate(from, to, bcc, subject, bodyTemplate, root);
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