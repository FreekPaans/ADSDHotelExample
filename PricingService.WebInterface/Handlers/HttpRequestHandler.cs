using CustomerWebsite.Events;
using Infrastructure.HTTP.ProcessingPipeline;
using ReservationService.Contracts.Events.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;

namespace PricingService.WebInterface.Handlers {
	public class HttpRequestHandler  : IHandleHttpRequests, 
			IHandleHttpProcessingEvents<RoomTypeIDsAvailable> {
		readonly static Random Rnd = new Random();
		public void Handle(HttpProcessingPipelineContext context,RoomTypeIDsAvailable @event) {
			foreach(var roomType in @event.RoomTypeIds) {
				@event.CurrentView.XPathSelectElement(string.Format("//*[@id='{0}']",roomType)).Add(
						XElement.Parse(string.Format("<div class='price_block'><div>Starting at</div><div class='price'>${0:N2}</div></div>", Rnd.Next(300)))
				);
			}
		}

		public void HandleHttpRequest(HttpProcessingPipelineContext httpContext) {
			
		}
	}
}