using CustomerWebsite.Contracts.Events;
using ReservationService.Contracts.Events.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerWebsite.WebInterface.Controllers {
	public class RoomsController:ADSDControllerBase {
		public ActionResult Search(DateTime search_from, DateTime search_till) {
			ProcessingContext.Dispatch(new SearchingForRooms { From = search_from, Till = search_till });
			ViewBag.BodyClasses = "Rooms_Search"; 
			//var view =View();

			return View();
		}
	}
}
