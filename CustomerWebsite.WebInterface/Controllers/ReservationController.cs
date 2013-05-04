using CustomerWebsite.Events;
using ReservationService.Contracts.Events.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerWebsite.WebInterface.Controllers {
	public class ReservationController:ADSDControllerBase{
		[HttpPost]
		public ActionResult Start(Guid roomTypeId, DateTime from,DateTime till ) {
			var id = Guid.NewGuid();
			ProcessingContext.Dispatch(new StartingNewReservation { 
				ReservationId = id,
				RoomTypeId = roomTypeId,
				From = from,
				Till = till

			}  );
			return RedirectToAction("ObtainDetails", new { reservationId = id});
		}

		public ActionResult ObtainDetails(Guid reservationId) {
			ProcessingContext.Dispatch(new ObtainingReservationDetails { ReservationId = reservationId });
			return View("~/Views/Home/Index.cshtml");
		}
	}
}
