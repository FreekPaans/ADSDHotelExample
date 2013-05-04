using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationService.WebInterface.Models {
	public class SearchParameters {
		public DateTime From{get;set;}
		public DateTime Till{get;set;}
	}
}