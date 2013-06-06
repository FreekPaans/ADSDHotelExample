using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occupancy.Contracts.Events {
	public class ReservationRoomOccupied : IEvent{
		public Guid ReservationId { get; set; }

		public bool Tentative{ get; set; }
	}
}
