using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Commands {
	public class StoreRervationBillingInformation : ICommand{
		public Guid ReservationId { get; set; }

		public string CreditCardNumber { get; set; }
	}
}
