using Infrastructure.HTTP.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontdesk.WebInterface.Controllers {
	public class HomeController:ServicePipelineControllerBase {
		public ActionResult Index() {
			return View();
		}
	}
}
