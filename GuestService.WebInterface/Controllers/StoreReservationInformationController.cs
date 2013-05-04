using GuestService.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuestService.WebInterface.Controllers {
	public class StoreReservationInformationController:Controller {
		readonly GuestDatabase _database;
		public StoreReservationInformationController(GuestDatabase database) {
			_database = database;
		}

		public class StoreGuestInfo {
			public Guid ReservationId{get;set;}
			public string Firstname{get;set;}
			public string Lastname{get;set;}
			public string Phonenumber{get;set;}
			public string Email{get;set;}
		}
		public ActionResult Index(StoreGuestInfo guestService) {
			_database.StoreGuestInfo(guestService.ReservationId,guestService.Firstname,guestService.Lastname,guestService.Phonenumber,guestService.Email);
			return Content("OK");
		}

	}
}
