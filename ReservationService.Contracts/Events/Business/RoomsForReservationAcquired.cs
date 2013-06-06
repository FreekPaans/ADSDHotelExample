using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Contracts.Events.Business {
	public class RoomsForReservationAcquired : IEvent {
		public Guid ReservationId{get;set;}
	}
}
