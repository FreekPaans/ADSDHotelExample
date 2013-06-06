using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Contracts.Events.Business {
	public class ReservationAccepted : IEvent{
		public Guid ReservationId{get;set;}

		public Guid RoomTypeid { get; set; }

		public DateTime Checkout { get; set; }

		public DateTime Arrival { get; set; }
	}
}
