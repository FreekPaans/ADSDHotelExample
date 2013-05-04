using Infrastructure.HTTP.ProcessingPipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerWebsite.WebInterface.Controllers {
	public class HomeController:ADSDControllerBase {
		//readonly IHttpProcessingPipeline _processingPipeline;
		
		public HomeController() {
			//_processingPipeline = processingPipeline;
		}
			
		public ActionResult Index() {
			return View();
		}

	}
}
