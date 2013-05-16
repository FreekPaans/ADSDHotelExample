using CustomerWebsite.Contracts.ReservationSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomTypeDetailsService.WebInterface.Configuration {
	class RoomTypeDetailsProvider:IProvideRoomTypeDetails {
		public RoomTypeDetails GetRoomTypeDetails(Guid roomTypeId) {
			return new RoomTypeDetails  { RoomType = "Queen" };
		}
	}
}
