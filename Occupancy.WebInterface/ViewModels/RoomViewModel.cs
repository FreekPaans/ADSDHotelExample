using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Occupancy.WebInterface.ViewModels {
	public class RoomViewModel {
		public string RoomNumber { get; set; }

		public Guid RoomType { get; set; }

		public bool Selected{get;set;}
	}
}