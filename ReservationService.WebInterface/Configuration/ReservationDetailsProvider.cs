using CustomerWebsite.Contracts.Events;
using CustomerWebsite.Contracts.ReservationSummary;
using Infrastructure.HTTP.Session;
using ReservationService.Backend.Commands;
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
			var originalCommand = (StartReservation)_sessionStorage[reservationId];

			return new ReservationDetails {
				ArrivalDate = originalCommand.From,
				CheckoutDate = originalCommand.Till,
				RoomTypeId = originalCommand.RoomTypeId ,
				DateBooked = originalCommand.DateBooked
			};
		}
	}
}
