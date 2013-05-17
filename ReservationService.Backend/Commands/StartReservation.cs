using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Backend.Commands {
	public class StartReservation : ICommand{
		public Guid ReservationId { get; set; }

		public DateTime Till { get; set; }

		public Guid RoomTypeId { get; set; }

		public DateTime From { get; set; }

		public DateTime DateBooked{ get; set; }
	}
}
