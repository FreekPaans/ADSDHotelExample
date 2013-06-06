using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Backend.Commands {
	public class StartCheckIn : ICommand{
		public Guid ReservationId { get; set; }

		//this field allows us to test checkin time specific issues better
		public DateTime? DateContext { get; set; }
	}
}
