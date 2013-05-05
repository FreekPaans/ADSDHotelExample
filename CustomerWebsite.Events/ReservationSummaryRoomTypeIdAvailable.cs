using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerWebsite.Events {
	public class ReservationSummaryRoomTypeIdAvailable {
		public Guid RoomTypeId { get; set; }

		public System.Xml.Linq.XElement Element { get; set; }
	}
}
