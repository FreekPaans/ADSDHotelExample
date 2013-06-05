using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frontdesk.Contracts.ReservationCheckin {
	public interface IProvideGuestDetails {
		GuestDetails GetGuestDetails(Guid reservationId);
	}

	public class GuestDetails  {
		public string Name{get;set;}
	}
}
