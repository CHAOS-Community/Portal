using Amazon.SimpleEmail.Model;

namespace Chaos.Portal.Core.EmailService
{
	public interface IEmailSender
	{
		SendEmailResponse Send(SendEmailRequest request);
	}
}