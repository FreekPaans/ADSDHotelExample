using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerWebsite.Contracts.ReservationSummary {
	public interface IProvideGuestDetails {	
		GuestDetails GetGuestDetails(Guid reservationId);
	}
	public class GuestDetails {
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phonenumber { get; set; }
	}
}
