using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frontdesk.Contracts.ReservationCheckin {
	public interface IProvideGuestDetails {
		GuestDetails GetGuestDetails(Guid reservationId);

		ICollection<GuestDetails> GetGuestsDetails(ICollection<Guid> reservationIds);
	}

	public class GuestDetails  {
		public Guid ReservationId{get;set;}
		public string Name{get;set;}
	}
}
