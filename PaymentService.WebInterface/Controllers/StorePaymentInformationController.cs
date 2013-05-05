using Infrastructure.Messaging;
using PaymentService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaymentService.WebInterface.Controllers {
	public class StorePaymentInformationController:Controller {
		readonly ICommandBus _commandBus;
		public StorePaymentInformationController(ICommandBus commandBus) {
			_commandBus = commandBus;
		}

		public ActionResult Index(StoreRervationBillingInformation  paymentService) {
			//_commandBus.Send(paymentService);
			return Content("OK");
		}

	}
}
