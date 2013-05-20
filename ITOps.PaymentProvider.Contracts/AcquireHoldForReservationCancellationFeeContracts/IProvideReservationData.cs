using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOps.PaymentProvider.Contracts.AcquireHoldForReservationCancellationFeeContracts {
	public interface IProvideReservationData {
		ReservationData GetReservationData(Guid reservationId);
	}

	public class ReservationData {
		public DateTime Arrival{get;set;}
		public DateTime Checkout{get;set;}
		public Guid RoomTypeId{get;set;}

		public DateTime ReservedAt { get; set; }
	}
}
