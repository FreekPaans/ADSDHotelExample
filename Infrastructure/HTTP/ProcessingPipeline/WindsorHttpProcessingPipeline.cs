using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.HTTP.ProcessingPipeline {
	public class WindsorHttpProcessingPipeline  : IHttpProcessingPipeline{
		readonly IWindsorContainer _container;
		public WindsorHttpProcessingPipeline(IWindsorContainer container) {
			_container = container;
		}
		public void HandleRequest(HttpProcessingPipelineContext processingContext) {
			var handlers = _container.ResolveAll<IHandleHttpRequests>();
			try {
				processingContext.Run(handlers);	
			} 
			finally {
				foreach(var handler in handlers) {
					_container.Release(handler);
				}
			}
		}
	}
}
