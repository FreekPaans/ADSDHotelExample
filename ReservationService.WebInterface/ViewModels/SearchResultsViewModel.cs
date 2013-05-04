using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservationService.WebInterface.ViewModels {
	public class SearchResultsViewModel {
		public ICollection<Guid> RoomTypeIds{get;set;}
		public DateTime From{get;set;}
		public DateTime Till{get;set;}
	}
}
