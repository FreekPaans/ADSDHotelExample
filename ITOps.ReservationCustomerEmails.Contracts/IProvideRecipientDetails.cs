using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITOps.ReservationCustomerEmails.Contracts {
	public interface IProvideRecipientDetails {
		RecipientDetails GetDetails(Guid reservationId);
	}

	public class RecipientDetails {
		public System.Net.Mail.MailAddress MailAddress { get; set; }

		public string Name { get; set; }
	}
}
