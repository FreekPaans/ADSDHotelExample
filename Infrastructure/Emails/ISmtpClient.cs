using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Emails {
	public interface ISmtpClient {
		void Send(MailMessage message);
	}
}
