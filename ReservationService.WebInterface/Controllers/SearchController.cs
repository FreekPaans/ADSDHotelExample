using ReservationService.WebInterface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservationService.WebInterface.Controllers {
	public class SearchController : Controller{
		public ActionResult Index() {
			return View("~/Views/SearchRoomsBox.cshtml");
		}

		public ActionResult Results() {
			return View("~/Views/SearchResultsView.cshtml", new SearchResultsViewModel { RoomTypeIds = new [] { Guid.NewGuid(), Guid.NewGuid() }});
		}
	}
}