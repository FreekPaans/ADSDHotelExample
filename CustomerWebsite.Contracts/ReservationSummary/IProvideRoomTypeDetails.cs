using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerWebsite.Contracts.ReservationSummary {
	public interface IProvideRoomTypeDetails {
		RoomTypeDetails GetRoomTypeDetails(Guid roomTypeId);
	}
	public class RoomTypeDetails {
		public string RoomType{get;set;}
	}
}
