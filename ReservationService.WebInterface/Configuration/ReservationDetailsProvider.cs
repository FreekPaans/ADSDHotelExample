using CustomerWebsite.Contracts.ReservationSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReservationService.WebInterface.Configuration {
	class ReservationDetailsProvider:IProvideReservationDetails {
		public ReservationDetails GetReservationDetails(Guid reservationId) {
			return new ReservationDetails { ArrivalDate = DateTime.Now, CheckoutDate = DateTime.Now.AddDays(5), RoomTypeId = Guid.NewGuid() };
		}
	}
}
