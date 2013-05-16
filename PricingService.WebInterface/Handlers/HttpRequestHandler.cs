using CustomerWebsite.Contracts.Events;
using Infrastructure.HTTP.ProcessingPipeline;
using PricingService.WebInterface.Views;
using ReservationService.Contracts.Events.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;

namespace PricingService.WebInterface.Handlers {
	public class HttpRequestHandler  : IHandleHttpRequests, 
			IHandleHttpProcessingEvents<RoomTypeIDsAvailable>,
			IHandleHttpProcessingEvents<SearchingForRooms>
			 {
		readonly static Random Rnd = new Random();

		public void Handle(HttpProcessingPipelineContext context,RoomTypeIDsAvailable @event) {
			AssertIsSearchingForRooms();

			context.WriteView("Pricing_RoomTypeIDsAvailable_RoomPrices",() => new RoomPricing {
				PricesForRoom = 
				@event.RoomTypeIds.Select(r=>Tuple.Create(r,(decimal)Rnd.Next(300))).ToArray() 
			}.TransformText());
		}

		private void AssertIsSearchingForRooms() {
			if(_searchingForRooms==null) {
				throw new InvalidOperationException("Not searching for rooms, can't determine price");
			}
		}

		public void HandleHttpRequest(HttpProcessingPipelineContext httpContext) {
			
		}

		SearchingForRooms _searchingForRooms;

		public void Handle(HttpProcessingPipelineContext context,SearchingForRooms @event) {
			_searchingForRooms = @event;
		}
	}
}