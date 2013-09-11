using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Xml.Linq;
using Amazon.SimpleEmail.Model;
using Chaos.Portal.Core.EmailService;
using Moq;
using NUnit.Framework;

namespace Chaos.Portal.Test.EmailService
{
	[TestFixture]
	public class AWSEmailServiceTest
	{
		[Test]
		public void Send_GivenSingleTo_SendSingleEmail()
		{
			var senderMock = new Mock<IEmailSender>();
			var service = new Portal.EmailService.EmailService(senderMock.Object);
			
			const string to = "to@test.com";
			const string from = "from@test.com";
			const string subject = "Test Email";
			const string body = "<div>Hallo test</div>";

			SendEmailRequest request = null;

			senderMock.Setup(s => s.Send(It.IsAny<SendEmailRequest>())).Callback<SendEmailRequest>(r => request = r);

			service.Send(from, to, subject, body);

			senderMock.Verify( s => s.Send(It.IsAny<SendEmailRequest>()), Times.Once());

			Assert.That(request, Is.Not.Null);
			Assert.That(request.Source, Is.EqualTo(from));
			Assert.That(request.Destination.BccAddresses, Is.Not.Null);
			Assert.That(request.Destination.BccAddresses.Count, Is.EqualTo(1));
			Assert.That(request.Destination.BccAddresses.First(), Is.EqualTo(to));
			Assert.That(request.Message.Subject.Data, Is.EqualTo(subject));
			Assert.That(request.Message.Body.Html.Data, Is.EqualTo(body));
		}

		[Test]
		public void Send_Given51Tos_SendTwoBatches()
		{
			var senderMock = new Mock<IEmailSender>();
			var service = new Portal.EmailService.EmailService(senderMock.Object);

			var tos = new List<string>();
			const string from = "from@test.com";
			const string subject = "Test Email";
			const string body = "<div>Hallo test</div>";

			for (var i = 0; i < 51; i++)
				tos.Add(string.Format("MyMail{0}@test.test", i));

			IList<SendEmailRequest> request = new List<SendEmailRequest>();

			senderMock.Setup(s => s.Send(It.IsAny<SendEmailRequest>())).Callback<SendEmailRequest>(request.Add);

			service.Send(from, tos, subject, body);

			senderMock.Verify(s => s.Send(It.IsAny<SendEmailRequest>()), Times.Exactly(2));

			Assert.That(request.Count, Is.EqualTo(2));
			Assert.That(request.First().Destination.BccAddresses.Count, Is.EqualTo(50));
			Assert.That(request.Skip(1).First().Destination.BccAddresses.Count, Is.EqualTo(1));
		}

		[Test]
		public void Send_GivenTemplate_TransformData()
		{
			var senderMock = new Mock<IEmailSender>();
			IEmailService service = new Portal.EmailService.EmailService(senderMock.Object);

			const string to = "to@test.com";
			const string from = "from@test.com";
			const string subject = "Test Email";
			var template = XElement.Load("EmailService/EmailTemplate01.xml");
			var data = XElement.Parse("<Person><Name>Albert Einstein</Name></Person>");

			SendEmailRequest request = null;

			senderMock.Setup(s => s.Send(It.IsAny<SendEmailRequest>())).Callback<SendEmailRequest>(r => request = r);

			service.SendTemplate(from, to, subject, template, data);

			Assert.That(request, Is.Not.Null);
			Assert.That(request.Message.Body.Html.Data, Is.EqualTo("<html>\r\n  <body>\r\n    <h1>Hallo</h1>\r\n    <p>\r\n\t\t\t\t\tHow are you Albert Einstein?\r\n\t\t\t\t</p>\r\n  </body>\r\n</html>"));
		}

		[Test]
		public void Send_GivenTemplateAndMultipleData_TransformData()
		{
			var senderMock = new Mock<IEmailSender>();
			IEmailService service = new Portal.EmailService.EmailService(senderMock.Object);

			const string to = "to@test.com";
			const string from = "from@test.com";
			const string subject = "Test Email";
			var template = XElement.Load("EmailService/EmailTemplate02.xml");
			var datas = new List<XElement>
			{
				XElement.Parse("<Person><Name>Richard Dawkins</Name></Person>"),
				XElement.Parse("<Order><Name>The God Delusion</Name></Order>")
			};

			SendEmailRequest request = null;

			senderMock.Setup(s => s.Send(It.IsAny<SendEmailRequest>())).Callback<SendEmailRequest>(r => request = r);

			service.SendTemplate(from, to, subject, template, datas);

			Assert.That(request, Is.Not.Null);
			Assert.That(request.Message.Body.Html.Data, Is.EqualTo("<html>\r\n  <body>\r\n    <h1>Hallo</h1>\r\n    <p>\r\n\t\t\t\t\tHow are you Richard Dawkins?\r\n\t\t\t\t</p>\r\n    <p>\r\n\t\t\t\t\tYou ordered The God Delusion.\r\n\t\t\t\t</p>\r\n  </body>\r\n</html>"));
		}
	}
}