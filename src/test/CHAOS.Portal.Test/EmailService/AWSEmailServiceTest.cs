using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Chaos.Portal.EmailService;
using Moq;
using NUnit.Framework;

namespace Chaos.Portal.Test.EmailService
{
	[TestFixture]
	public class AWSEmailServiceTest
	{
		[Test]
		public void Send_GivenSingleEmail_SendSingleEmail()
		{
			return; //TODO: Find a way to mock AmazonSimpleEmailServiceClient

			var awsMock = new Mock<AmazonSimpleEmailServiceClient>("id", "secret");
			var service = new AWSEmailService(awsMock.Object);
			
			var to = "to@test.com";
			var from = "from@test.com";
			var subject = "Test Email";
			var body = "<div>Hallo test</div>";

			awsMock.Setup(a => a.SendEmail(It.IsAny<SendEmailRequest>()));

			service.Send(from, to, subject, body);

			awsMock.Verify(a => a.SendEmail(It.IsAny<SendEmailRequest>()));
		}
	}
}