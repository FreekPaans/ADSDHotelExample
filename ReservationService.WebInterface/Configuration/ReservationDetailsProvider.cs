using CustomerWebsite.Contracts.Events;
using CustomerWebsite.Contracts.ReservationSummary;
using Infrastructure.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReservationService.WebInterface.Configuration {
	class ReservationDetailsProvider:IProvideReservationDetails {
		private ISessionStorage _sessionStorage;
		public ReservationDetailsProvider(ISessionStorage sessionStorage) {
			_sessionStorage = sessionStorage;
		}


		public ReservationDetails GetReservationDetails(Guid reservationId) {
			var originalEvent = (StartingNewReservation)_sessionStorage[reservationId];

			return new ReservationDetails { ArrivalDate = originalEvent.From, CheckoutDate = originalEvent.Till, RoomTypeId = originalEvent.RoomTypeId };
		}
	}
}
