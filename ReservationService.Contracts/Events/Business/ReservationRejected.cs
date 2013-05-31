using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Contracts.Events.Business {
	public class ReservationRejected : IEvent{
		public Guid ReservationId { get; set; }
		public const string BecauseCancellationFeeHoldDenied = "CancellationFeeHoldDenied";

		public string Reason { get; set; }
	}
}
