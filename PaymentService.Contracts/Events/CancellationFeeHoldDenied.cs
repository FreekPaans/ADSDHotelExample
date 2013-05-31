using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Contracts.Events {
	public class CancellationFeeHoldDenied : IEvent{
		public Guid ReservationId { get; set; }
	}
}
