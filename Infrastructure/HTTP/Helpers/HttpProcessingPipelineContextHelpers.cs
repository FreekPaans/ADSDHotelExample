using Infrastructure.HTTP.ProcessingPipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.HTTP.Helpers {
	public static class HttpProcessingPipelineContextHelpers {
		public static string GetString(this HttpProcessingPipelineContext context, string paramName) {
			
			return context.HttpContext.Request.Params[paramName];
		}

		public static Guid GetGuid(this HttpProcessingPipelineContext context, string paramName) {
			return Guid.Parse(context.GetString(paramName));
		}
	}
}
