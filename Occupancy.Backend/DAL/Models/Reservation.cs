using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occupancy.Backend.DAL.Models {
	public class Reservation {
		public Guid ReservationId{get;set;}
		public DateTime Arrival{get;set;}
		public DateTime Checkout{get;set;}

		public bool RoomAcquired { get; set; }
		public byte[] Version{get;set;}

		public bool GuestArrived { get; set; }
	}
}
