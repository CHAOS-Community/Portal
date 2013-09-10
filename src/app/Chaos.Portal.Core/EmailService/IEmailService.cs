using System.Collections.Generic;

namespace Chaos.Portal.Core.EmailService
{
	public interface IEmailService
	{
		void Send(string from, string to, string subject, string body);
		void Send(string from, IEnumerable<string> to, string subject, string body);

		void SendWithXSLT(string from, string to, string subject, string bodyTemplate, string bodyData);
		void SendWithXSLT(string from, IEnumerable<string> to, string subject, string bodyTemplate, string bodyData);
	}
}