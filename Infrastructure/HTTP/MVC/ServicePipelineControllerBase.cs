﻿using Infrastructure.HTTP.ProcessingPipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Infrastructure.HTTP.MVC {
	public class ServicePipelineControllerBase : Controller{
		//this is public for DI
		public IHttpProcessingPipeline ProcessingPipeline { get; set; }

		protected HttpProcessingPipelineContext ProcessingContext { get; set; }

		protected override void OnActionExecuting(ActionExecutingContext filterContext) {
			base.OnActionExecuting(filterContext);

			var ctx = new HttpProcessingPipelineContext(filterContext);

			ProcessingPipeline.HandleRequest(ctx);

			ProcessingContext = ctx;
			ViewBag.HttpProcessingPipelineContext = ctx;

			ViewBag.RenderSection = new Func<string,MvcHtmlString>(s => ctx.RenderSection(s));
		}

		protected override void OnResultExecuted(ResultExecutedContext filterContext) {
			base.OnResultExecuted(filterContext);

			if(ProcessingPipeline is IDisposable) {
				((IDisposable)ProcessingPipeline).Dispose();
			}
		}
	}
}
