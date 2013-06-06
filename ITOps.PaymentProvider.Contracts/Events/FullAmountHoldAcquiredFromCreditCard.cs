using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOps.PaymentProvider.Contracts.Events {
	public class FullAmountHoldAcquiredFromCreditCard : IEvent{
		public Guid ReservationId { get; set; }
	}
}
