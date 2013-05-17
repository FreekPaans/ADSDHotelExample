using Infrastructure.Messaging;
using ReservationService.Backend.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservationService.WebInterface.Controllers {
	public class CommitController:Controller {
		readonly ICommandBus _commandBus;

		public CommitController(ICommandBus commandBus) {
			_commandBus = commandBus;
		}
		public ActionResult Index(CommitReservation reservation) {
			//_commandBus.Send(reservation);
			return Redirect("/Reservation/ThankYou?reservationId="+reservation.ReservationId.ToString());
		}

	}
}
