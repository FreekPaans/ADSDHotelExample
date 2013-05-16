using CustomerWebsite.Events;
using CustomerWebsite.WebInterface.ViewModels;
using ReservationService.Contracts.Events.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.XPath;

namespace CustomerWebsite.WebInterface.Controllers {
	public class ReservationController:ADSDControllerBase{
		readonly ReservationSummaryViewModel.Provider _summaryProvider;

		public ReservationController(ReservationSummaryViewModel.Provider summaryProvider) {
			_summaryProvider = summaryProvider;
		}

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
			ProcessingContext.Dispatch(new ObtainingReservationDetails { 
				ReservationId = reservationId,
				SubmitUrl = string.Format("/Reservation/Summary?reservationId={0}",reservationId)
			});
			return View();
		}

		public ActionResult Summary(Guid reservationId) {
			return View(_summaryProvider.GetSummary(reservationId));
		}
	}
}
