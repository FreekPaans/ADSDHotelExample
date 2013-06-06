using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Occupancy.Contracts.Events {
	public class CheckInToRoomFailedBecauseRoomIsAlreadyOccupied : IEvent{
		public Guid ReservationId { get; set; }
	}
}
