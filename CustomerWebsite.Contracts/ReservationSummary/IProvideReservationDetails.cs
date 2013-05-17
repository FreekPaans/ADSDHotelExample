using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerWebsite.Contracts.ReservationSummary {
	public interface IProvideReservationDetails {
		ReservationDetails GetReservationDetails(Guid reservationId);
	}

	public class ReservationDetails{
		public DateTime ArrivalDate{get;set;}
		public DateTime CheckoutDate{get;set;}
		public Guid RoomTypeId{get;set;}

		public DateTime DateBooked { get; set; }
	}
}
