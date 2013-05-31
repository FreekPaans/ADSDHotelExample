using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentService.Contracts.Events {
	public class CancellationFeeHoldAcquired : IEvent{
		public Guid ReservationId { get; set; }
	}
}
