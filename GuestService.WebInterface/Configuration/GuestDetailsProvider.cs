using CustomerWebsite.Contracts.ReservationSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuestService.WebInterface.Configuration {
	class GuestDetailsProvider:IProvideGuestDetails {
		public GuestDetails GetGuestDetails(Guid reservationId) {
			return new GuestDetails { Email = "freek@infi.nl", Name = "Freek Paans", Phonenumber = "0647897897" };
		}
	}
}
