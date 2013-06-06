using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frontdesk.Contracts.Events {
	public class StartingCheckIn {
		public Guid ReservationId { get; set; }
	}
}
