using GuestService.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace GuestService.DataProviders{
	class GuestDetailsProvider:
			CustomerWebsite.Contracts.ReservationSummary.IProvideGuestDetails,
			ITOps.ReservationCustomerEmails.Contracts.IProvideRecipientDetails,
			Frontdesk.Contracts.ReservationCheckin.IProvideGuestDetails
		{
		readonly GuestDataContext _context;
		public GuestDetailsProvider(GuestDataContext context) {
			_context = context;
		}
		public CustomerWebsite.Contracts.ReservationSummary.GuestDetails GetGuestDetails(Guid reservationId) {
			var guest= _context.Guests.Find(reservationId);
			return new CustomerWebsite.Contracts.ReservationSummary.GuestDetails { 
				Email = guest.Email, 
				Name = FormatName(guest),
				Phonenumber = guest.Phonenumber
			};
		}

		public ITOps.ReservationCustomerEmails.Contracts.RecipientDetails GetDetails(Guid reservationId) {
			var guest = _context.Guests.Find(reservationId);

			var name = FormatName(guest);

			return new ITOps.ReservationCustomerEmails.Contracts.RecipientDetails {
				MailAddress = new MailAddress(guest.Email,name),
				Name = name
			};
		}

		private static string FormatName(Backend.DAL.Models.Guest guest) {
			return string.Format("{0} {1}",guest.Firstname,guest.Lastname);
		}

		Frontdesk.Contracts.ReservationCheckin.GuestDetails Frontdesk.Contracts.ReservationCheckin.IProvideGuestDetails.GetGuestDetails(Guid reservationId) {
			return ((Frontdesk.Contracts.ReservationCheckin.IProvideGuestDetails)this).GetGuestsDetails(new Guid[] { reservationId}).First();
		}


		ICollection<Frontdesk.Contracts.ReservationCheckin.GuestDetails> Frontdesk.Contracts.ReservationCheckin.IProvideGuestDetails.GetGuestsDetails(ICollection<Guid> reservationIds) {
			var guests = _context.Guests.Where(g=>reservationIds.Contains(g.ReservationId)).ToArray();

			return guests.Select(guest=>new Frontdesk.Contracts.ReservationCheckin.GuestDetails {
				Name = FormatName(guest),
				ReservationId = guest.ReservationId
			}).ToArray();
		}
	}
}
