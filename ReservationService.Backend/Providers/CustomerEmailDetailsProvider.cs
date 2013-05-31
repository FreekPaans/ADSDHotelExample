using ITOps.ReservationCustomerEmails.Contracts;
using ReservationService.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Backend.Providers {
	class CustomerEmailDetailsProvider:IProvideReservationDetails {
		readonly ReservationDataContext _context;
		public CustomerEmailDetailsProvider(ReservationDataContext context) {
			_context = context;
		}
		public ReservationDetails GetDetails(Guid reservationId) {
			var reservation = _context.Reservations.Find(reservationId);
			return new ReservationDetails {
				ArrivalDate = reservation.From,
				CheckoutDate = reservation.To
			};
		}
	}
}
