using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.HTTP.ProcessingPipeline {
	public interface IHandleHttpProcessingEvents<T> where T:class{
		void Handle(HttpProcessingPipelineContext context, T @event);
	}
}
