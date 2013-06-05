using Frontdesk.Contracts.ReservationCheckin;
using Frontdesk.WebInterface.ViewModels;
using Infrastructure.HTTP.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontdesk.WebInterface.Controllers {
	public class HomeController:ServicePipelineControllerBase {
		readonly IProvideReservationDetails _reservationDetailsProvider;
		readonly IProvideGuestDetails _guestDetailsProvider;	

		public HomeController(IProvideReservationDetails reservationDetailsProvider, IProvideGuestDetails guestDetailsProvider) {
			_reservationDetailsProvider = reservationDetailsProvider;	
			_guestDetailsProvider = guestDetailsProvider;
		}

		public ActionResult Index() {
			return View();
		}

		public ActionResult RenderReservation(Guid reservationId) {
			var model = new CheckinReservationViewModel { 
				ReservationDetails = _reservationDetailsProvider.GetReservationDetails(reservationId),
				GuestDetails = _guestDetailsProvider.GetGuestDetails(reservationId)
			};
			return View(model);
		}
	}
}
