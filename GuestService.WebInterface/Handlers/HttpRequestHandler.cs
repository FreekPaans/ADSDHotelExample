using GuestService.WebInterface.Views;
using Infrastructure.HTTP.ProcessingPipeline;
using ReservationService.Contracts.Events.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Infrastructure.HTTP.Helpers;
using GuestService.Logic;
using CustomerWebsite.Contracts.Events;

namespace GuestService.WebInterface.Handlers {
	public class HttpRequestHandler : IHandleHttpRequests, 
		IHandleHttpProcessingEvents<ObtainingReservationDetails> {

		public void HandleHttpRequest(HttpProcessingPipelineContext httpContext) {
		}

		public void Handle(HttpProcessingPipelineContext context, ObtainingReservationDetails @event) {
			context.WriteView("Guest_ObtainReservationDetails", ()=>new GuestDetailsForm  {ReservationId = @event.ReservationId}.TransformText());
		}
	}
}