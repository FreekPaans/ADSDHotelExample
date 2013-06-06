using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occupancy.Backend.DAL.Models {
	public class RoomOccupancy {
		public string RoomNumber{get;set;}
		public DateTime Date{get;set;}
		public Guid ReservationId{get;set;}

	}
}