using ReservationService.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.DataProviders {
	class CustomerEmailDetailsProvider:
			Frontdesk.Contracts.ReservationCheckin.IProvideReservationDetails,
			ITOps.ReservationCustomerEmails.Contracts.IProvideReservationDetails {
		readonly ReservationDataContext _context;
		public CustomerEmailDetailsProvider(ReservationDataContext context) {
			_context = context;
		}
		

		Frontdesk.Contracts.ReservationCheckin.ReservationDetails Frontdesk.Contracts.ReservationCheckin.IProvideReservationDetails.GetReservationDetails(Guid reservationId) {
			var reservation = _context.Reservations.Find(reservationId);
			
			return new Frontdesk.Contracts.ReservationCheckin.ReservationDetails {
				CanCheckin = reservation.CanCheckIn,
				CheckinDate = reservation.From,
				CheckoutDate = reservation.To,
				RoomTypeId =reservation.RoomTypeId,
				Status = reservation.GetFriendlyStatus()
			};
		}

		ITOps.ReservationCustomerEmails.Contracts.ReservationDetails ITOps.ReservationCustomerEmails.Contracts.IProvideReservationDetails.GetDetails(Guid reservationId) {
			var reservation = _context.Reservations.Find(reservationId);
			return new ITOps.ReservationCustomerEmails.Contracts.ReservationDetails {
				ArrivalDate = reservation.From,
				CheckoutDate = reservation.To
			};
		}
	}
}
