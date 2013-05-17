using Infrastructure.HTTP.ProcessingPipeline;
using ReservationService.Contracts.Events.UI;
using RoomTypeDetailsService.WebInterface.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;

namespace RoomTypeDetailsService.WebInterface.Handlers {
	public class HttpRequestHandler : IHandleHttpRequests , 
			IHandleHttpProcessingEvents<RoomTypeIDsAvailable>{
		public void HandleHttpRequest(HttpProcessingPipelineContext httpContext) {
			//throw new NotImplementedException();
		}

		public void Handle(HttpProcessingPipelineContext context, RoomTypeIDsAvailable @event) {
			
			var details=  @event.RoomTypeIds.Select(rt=>new ViewModels.RoomTypeDetailsViewModel {
				RoomTypeDescription = "Mauris imperdiet placerat magna, nec porta velit tempus nec. Aliquam posuere aliquam faucibus. Pellentesque euismod purus id velit aliquam fermentum. Nam ut blandit nisi.",
				RoomTypeName = rt == Guid.Parse("D9156B38-843E-43C3-BC82-7E4558BD1328")?"Penthouse": "Suite",
				RoomTypeImageUrl = "http://images.wikia.com/twilightsaga/images/2/2f/Mila-Kunis-1.jpg",
				RoomTypeId = rt
			}).ToArray();


			context.WriteView("RoomTypeDetails_RoomTypeIDsAvailable_RoomTypeDetails", ()=>new RoomTypeIDsAvailableView {
				Details = details
			}.TransformText());
		}
	}
}