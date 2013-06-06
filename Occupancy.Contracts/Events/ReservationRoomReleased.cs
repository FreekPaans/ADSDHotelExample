using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occupancy.Contracts.Events {
	public class ReservationRoomReleased : IEvent{
		public Guid ReservationId { get; set; }

		public string Reason { get; set; }
	}
}
