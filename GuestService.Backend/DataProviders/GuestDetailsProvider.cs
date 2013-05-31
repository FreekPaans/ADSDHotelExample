
using GuestService.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace GuestService.Backend.DataProviders{
	class GuestDetailsProvider:
			CustomerWebsite.Contracts.ReservationSummary.IProvideGuestDetails,
			ITOps.ReservationCustomerEmails.Contracts.IProvideRecipientDetails 
		{
		readonly GuestDataContext _context;
		public GuestDetailsProvider(GuestDataContext context) {
			_context = context;
		}
		public CustomerWebsite.Contracts.ReservationSummary.GuestDetails GetGuestDetails(Guid reservationId) {
			var guest= _context.Guests.Find(reservationId);
			return new CustomerWebsite.Contracts.ReservationSummary.GuestDetails { 
				Email = guest.Email, 
				Name = string.Format("{0} {1}", guest.Firstname,guest.Lastname), 
				Phonenumber = guest.Phonenumber
			};
		}

		public ITOps.ReservationCustomerEmails.Contracts.RecipientDetails GetDetails(Guid reservationId) {
			var guest = _context.Guests.Find(reservationId);

			var name = string.Format("{0} {1}", guest.Firstname, guest.Lastname);

			return new ITOps.ReservationCustomerEmails.Contracts.RecipientDetails {
				MailAddress = new MailAddress(guest.Email,name),
				Name = name
			};
		}
	}
}
