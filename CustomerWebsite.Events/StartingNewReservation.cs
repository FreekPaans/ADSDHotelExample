using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerWebsite.Events{
	public class StartingNewReservation {
		public Guid ReservationId { get; set; }

		public Guid RoomTypeId { get; set; }

		public DateTime From { get; set; }

		public DateTime Till { get; set; }
	}
}
