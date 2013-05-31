using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOps.ReservationCustomerEmails.Commands {
	public class SendReservationRejectedEmail: ICommand {
		public Guid ReservationId{get;set;}

		public string Reason { get; set; }
	}
}
