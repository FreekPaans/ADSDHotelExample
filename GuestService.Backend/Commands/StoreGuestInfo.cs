using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestService.Backend.Commands {
	public class StoreGuestInfo {
		public Guid ReservationId { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Phonenumber { get; set; }
		public string Email { get; set; }
	}
}
