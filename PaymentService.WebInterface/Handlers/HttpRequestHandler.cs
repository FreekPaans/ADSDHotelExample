using Infrastructure.HTTP.ProcessingPipeline;
using ReservationService.Contracts.Events.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.XPath;
using System.Xml.Linq;
using PaymentService.WebInterface.Views;
using Infrastructure.HTTP.Helpers;
using Infrastructure.Messaging;
using PaymentService.Commands;
using CustomerWebsite.Events;

namespace PaymentService.WebInterface.Handlers {
	public class HttpRequestHandler : IHandleHttpRequests, 
		IHandleHttpProcessingEvents<ObtainingReservationDetails>{
		readonly ICommandBus _commandBus;
		public HttpRequestHandler(ICommandBus commandBus) {
			_commandBus = commandBus;
		}

		public void Handle(HttpProcessingPipelineContext context, ObtainingReservationDetails @event) {
			context.WriteView("Payment_ObtainReservationDetails", ()=>
				new BillingInformationView {
					ReservationId = @event.ReservationId
				}.TransformText()
			);
		}

		public void HandleHttpRequest(HttpProcessingPipelineContext httpContext) {
		}

		
	}
}