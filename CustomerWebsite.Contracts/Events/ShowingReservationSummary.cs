using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerWebsite.Contracts.Events {
	public class ShowingReservationSummary {
		public Guid ReservationId { get; set; }
	}
}
