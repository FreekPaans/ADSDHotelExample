using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoomTypeDetailsService.WebInterface.ViewModels {
	public class RoomTypeDetailsViewModel {
		public Guid RoomTypeId{get;set;}
		public string RoomTypeName{get;set;}
		public string RoomTypeDescription{get;set;}
		public string RoomTypeImageUrl{get;set;}
	}
}