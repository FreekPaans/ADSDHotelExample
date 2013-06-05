using Infrastructure.HTTP.MVC;
using Infrastructure.HTTP.ProcessingPipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerWebsite.WebInterface.Controllers {
	public class HomeController:ServicePipelineControllerBase {
		public HomeController() {
		}
			
		public ActionResult Index() {
			return View();
		}
	}
}
