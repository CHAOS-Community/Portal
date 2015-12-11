using Amazon.SimpleEmail.Model;

namespace Chaos.Portal.Core.EmailService
{
	public interface IEmailSender
	{
		uint MaxRecipientPerBatch { get; }

		SendEmailResponse Send(SendEmailRequest request);
	}
}