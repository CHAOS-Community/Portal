using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Chaos.Portal.Core.EmailService;

namespace Chaos.Portal.EmailService
{
	public class AWSEmailSender : IEmailSender
	{
		private readonly AmazonSimpleEmailServiceClient _client;

		public uint MaxRecipientPerBatch { get { return 50; } }

		public AWSEmailSender(string accessKey, string secretKey)
		{
			_client = new AmazonSimpleEmailServiceClient(accessKey, secretKey);
		}

		public SendEmailResponse Send(SendEmailRequest request)
		{
			return _client.SendEmail(request);
		}
	}
}