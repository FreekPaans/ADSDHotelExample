using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReservationService.Contracts.Events.UI {
	public class RoomTypeIDsAvailable {
		public ICollection<Guid> RoomTypeIds { get; set; }
	}
}
