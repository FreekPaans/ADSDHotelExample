using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITOps.ReservationCustomerEmails.Commands {
	public class SendReservationAcceptedEmail : ICommand {
		public Guid ReservationId{get;set;}
	}
}
