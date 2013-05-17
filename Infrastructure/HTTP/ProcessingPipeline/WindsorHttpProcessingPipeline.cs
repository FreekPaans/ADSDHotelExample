using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.HTTP.ProcessingPipeline {
	public class WindsorHttpProcessingPipeline  : IHttpProcessingPipeline, IDisposable{
		readonly IWindsorContainer _container;
		private IHandleHttpRequests[] _handlers;

		public WindsorHttpProcessingPipeline(IWindsorContainer container) {
			_container = container;
		}
		public void HandleRequest(HttpProcessingPipelineContext processingContext) {
			_handlers = _container.ResolveAll<IHandleHttpRequests>();
			try {
				processingContext.Run(_handlers);	
			} 
			catch {
			}
		}

		public void Dispose() {
			if(_handlers==null) {	
				return;
			}
			foreach(var handler in _handlers) {
				_container.Release(handler);
			}
			
		}
	}
}
