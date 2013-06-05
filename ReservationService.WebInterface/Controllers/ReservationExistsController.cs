using ReservationService.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservationService.WebInterface.Controllers {
	public class ReservationExistsController:Controller {
		readonly ReservationFacade _facade;

		public ReservationExistsController(ReservationFacade facade) {
			_facade = facade;
		}

		public ActionResult Index(Guid reservationId) {
			return Json(_facade.ReservationExists(reservationId),JsonRequestBehavior.AllowGet);
		}

	}
}
