using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Infrastructure.HTTP.ProcessingPipeline {
	public class HttpProcessingPipelineContext {
		readonly System.Web.HttpContextBase _httpContext;

		public System.Web.HttpContextBase HttpContext {
			get { return _httpContext; }
		} 

		readonly Dictionary<string,Func<string>> _views;
		readonly ActionExecutingContext _actionContext;
		private  IHandleHttpRequests[] _handlers;

		public ActionExecutingContext ActionContext {
			get { return _actionContext; }
		} 

		public HttpProcessingPipelineContext(ActionExecutingContext actionContext) {
			_httpContext = actionContext.HttpContext;
			_views = new Dictionary<string,Func<string>>();
			_actionContext = actionContext;
		}

		public void WriteView(string sectionName, Func<string> view) {
			_views[sectionName] = view;
		}

		public MvcHtmlString RenderSection(string name) {
			if(!_views.ContainsKey(name)) {
				foreach(var handler in _handlers.OfType<OnDemandViewRenderer>()) {
					handler.DrawViewOnDemand(this, name);
				}
			}

			var res= _views[name]();
			_views.Remove(name);
			return new MvcHtmlString(WrapInIdedDiv(name,res));
		}

		private string WrapInIdedDiv(string name,string output) {
			return string.Format(@"<div id=""{0}"">{1}</div>", name,output);
		}

		public Dictionary<string,string> RenderSections() {
			return _views.ToDictionary(v=>v.Key,v=>WrapInIdedDiv(v.Key,v.Value()));
		}

		public void Dispatch<T>(T @event)  where T: class{
			HttpEventBus.Dispatch(this, _handlers,@event);
		}

		public void Run(IHandleHttpRequests[] handlers) {
			_handlers = handlers;
			
			foreach(var handler in handlers) {
				handler.HandleHttpRequest(this);
			}
			
		}
	}
}
