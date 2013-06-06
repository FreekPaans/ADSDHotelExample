using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Occupancy.Backend.Commands {
	public class CheckInToRoom : ICommand{
		public Guid ReservationId{get;set;}
		public string RoomNumber{get;set;}
	}
}
