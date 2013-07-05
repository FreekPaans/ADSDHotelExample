using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontdesk.WebInterface.ViewModels {
	public class ReservationsOverviewViewModel {
		public class ReservationOverviewViewModel {
			public Guid ReservationId { get; set; }

			public string Name { get; set; }

			public DateTime Arrival{ get; set; }
			public DateTime Checkout{ get; set; }
		}

		public ICollection<ReservationOverviewViewModel> Reservations { get; set; }
	}
}