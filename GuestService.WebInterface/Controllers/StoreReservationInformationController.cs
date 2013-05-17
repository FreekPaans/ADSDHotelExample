using GuestService.Backend;
using GuestService.Backend.Commands;
using GuestService.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuestService.WebInterface.Controllers {
	public class StoreReservationInformationController:Controller {
		readonly GuestServiceFacade _facade;
		
		public StoreReservationInformationController(GuestServiceFacade facade) {
			_facade = facade;
		}

		


		[HttpPost]
		public ActionResult Index(StoreGuestInfo guestService) {
			_facade.Handle(guestService);

			

			return Content("OK");
		}

	}
}
