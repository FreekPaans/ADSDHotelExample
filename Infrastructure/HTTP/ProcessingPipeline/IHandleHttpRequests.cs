using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.HTTP.ProcessingPipeline{
	public interface IHandleHttpRequests{
		void HandleHttpRequest(HttpProcessingPipelineContext httpContext);
	}

	public interface OnDemandViewRenderer {
		void DrawViewOnDemand(HttpProcessingPipelineContext context, string viewName);
	}
}
