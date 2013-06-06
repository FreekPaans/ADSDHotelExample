using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frontdesk.Contracts.ReservationCheckin {
	public interface IProvideReservationDetails {
		ReservationDetails GetReservationDetails(Guid reservationId);
	}

	public class ReservationDetails {
		public DateTime CheckinDate{get;set;}
		public DateTime CheckoutDate{get;set;}
		public bool CanCheckin{get;set;}
		public string Status{get;set;}
		public Guid RoomTypeId{get;set;}

		public bool FinishedCheckInProcess{ get; set; }
		public bool CheckedInSuccesfully { get; set; }
		public string CheckInFailedReason { get; set; }
	}
}
