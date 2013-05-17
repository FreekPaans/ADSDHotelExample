using Infrastructure.Messaging;
using PaymentService.Backend;
using PaymentService.Backend.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaymentService.WebInterface.Controllers {
	public class StorePaymentInformationController:Controller {
		readonly PaymentFacade _facade;
		public StorePaymentInformationController(PaymentFacade facade) {
			_facade = facade;
		}

		public ActionResult Index(StoreRervationBillingInformation  payment) {
			_facade.Handle(payment);
			return Content("OK");
		}
	}
}
