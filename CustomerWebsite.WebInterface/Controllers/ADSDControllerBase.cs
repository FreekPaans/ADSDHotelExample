using Infrastructure.HTTP.ProcessingPipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerWebsite.WebInterface.Controllers {
	public abstract class ADSDControllerBase:Controller {
		public IHttpProcessingPipeline ProcessingPipeline{get;set;}

		protected HttpProcessingPipelineContext ProcessingContext{get;set;}

		protected override void OnActionExecuting(ActionExecutingContext filterContext) {
			base.OnActionExecuting(filterContext);

			var ctx = new HttpProcessingPipelineContext(filterContext);

			ProcessingPipeline.HandleRequest(ctx);

			ProcessingContext = ctx;
			ViewBag.HttpProcessingPipelineContext = ctx;

			ViewBag.RenderSection = new Func<string,MvcHtmlString>(s=>ctx.RenderSection(s));
		}
	}
}
