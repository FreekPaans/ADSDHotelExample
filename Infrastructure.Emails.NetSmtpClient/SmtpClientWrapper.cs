using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Infrastructure.Emails.NetSmtpClient {
	public class SmtpClientWrapper : ISmtpClient {
		
		public void Send(System.Net.Mail.MailMessage message) {
			var client = new SmtpClient();
			
			//todo: config via configuration
			client.Host = "localhost";

			client.Send(message);
		}
	}
}
