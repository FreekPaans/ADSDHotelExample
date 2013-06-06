using Frontdesk.Contracts.Events;
using Frontdesk.Contracts.ReservationCheckin;
using Frontdesk.WebInterface.ViewModels;
using Infrastructure.HTTP.MVC;
using Infrastructure.Messaging;
using Occupancy.Backend.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontdesk.WebInterface.Controllers {
	public class ReservationController:ServicePipelineControllerBase {
		//
		// GET: /Reservation/

		readonly IProvideReservationDetails _reservationDetailsProvider;
		readonly IProvideGuestDetails _guestDetailsProvider;
		readonly ICommandBus _commandBus;
		
		public ReservationController(IProvideReservationDetails reservationDetailsProvider,IProvideGuestDetails guestDetailsProvider, ICommandBus commandBus) {
			_reservationDetailsProvider = reservationDetailsProvider;	
			_guestDetailsProvider = guestDetailsProvider;
			_commandBus = commandBus;
		}

		[HttpGet]
		public ActionResult Checkin(Guid reservationId) {
			ProcessingContext.Dispatch(new StartingCheckIn { ReservationId = reservationId });

			var model  = new CheckInReservationViewModel { 
				ReservationSummary =  GetSummaryViewModel(reservationId)
			};
			return View(model);
		}

		[HttpPost]
		public ActionResult Checkin(Guid reservationId, string selectedRoomNumber) {
			_commandBus.Send(new CheckInToRoom { ReservationId = reservationId, RoomNumber=selectedRoomNumber });
			
			return Content(Url.Action("CheckinStatus",new {reservationId = reservationId}),"text/plain");
		}

		public ActionResult CheckinStatus(Guid reservationId) {
			throw new HttpException(404, "Not checked in");
			return Json(false,JsonRequestBehavior.AllowGet);
		}

		public ActionResult RenderSearchSummary(Guid reservationId) {
			return View(GetSummaryViewModel(reservationId));
		}

		private ReservationSearchSummaryViewModel GetSummaryViewModel(Guid reservationId) {
			var model = new ReservationSearchSummaryViewModel {
				ReservationDetails = _reservationDetailsProvider.GetReservationDetails(reservationId),
				GuestDetails = _guestDetailsProvider.GetGuestDetails(reservationId),
				ReservationId = reservationId
			};
			return model;
		}

	}
}
