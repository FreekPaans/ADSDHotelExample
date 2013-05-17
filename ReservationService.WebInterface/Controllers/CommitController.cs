using Infrastructure.HTTP.Session;
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
		readonly ISessionStorage _sessionStorage;

		public CommitController(ICommandBus commandBus, ISessionStorage sessionStorage) {
			_commandBus = commandBus;
			_sessionStorage = sessionStorage ;
		}
		public ActionResult Index(CommitReservation reservation) {
			_commandBus.Send(reservation);
			return Redirect("/Reservation/ThankYou?reservationId="+reservation.ReservationId.ToString());
		}

	}
}
