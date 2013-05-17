using CustomerWebsite.Contracts.ReservationSummary;
using GuestService.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuestService.Backend.DataProviders{
	class GuestDetailsProvider:IProvideGuestDetails {
		readonly GuestDataContext _context;
		public GuestDetailsProvider(GuestDataContext context) {
			_context = context;
		}
		public GuestDetails GetGuestDetails(Guid reservationId) {
			var guest= _context.Guests.Find(reservationId);
			return new GuestDetails { Email = guest.Email, Name = string.Format("{0} {1}", guest.Firstname,guest.Lastname), Phonenumber = guest.Phonenumber};
		}
	}
}
