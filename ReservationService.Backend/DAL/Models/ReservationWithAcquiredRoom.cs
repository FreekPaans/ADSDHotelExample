using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Backend.DAL.Models {
	public class ReservationWithAcquiredRoom {
		[Key]
		public Guid ReservationId { get; set; }

		public Guid RoomTypeId { get; set; }

		public DateTime Arrival { get; set; }

		public DateTime Checkout { get; set; }
	}
}
