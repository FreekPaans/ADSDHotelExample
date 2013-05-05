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
	public class HttpRequestHandler : IHandleHttpRequests , IHandleHttpProcessingEvents<RoomTypeIDsAvailable>{
		public void HandleHttpRequest(HttpProcessingPipelineContext httpContext) {
			//throw new NotImplementedException();
		}

		public void Handle(HttpProcessingPipelineContext context, RoomTypeIDsAvailable @event) {
			//var currentView=  @event.CurrentView.ToString();
			
			//var xml = XDocument.Parse(currentView);

			foreach(var roomTypeId in @event.RoomTypeIds) {
				var room = @event.CurrentView.XPathSelectElement(string.Format("//*[@id='{0}']",roomTypeId));


				var roomXml = XDocument.Parse("<dummy>"+new RoomTypeSearchResultsDetailsView { 
					Model = new ViewModels.RoomTypeDetailsViewModel {
						RoomTypeDescription = "Mauris imperdiet placerat magna, nec porta velit tempus nec. Aliquam posuere aliquam faucibus. Pellentesque euismod purus id velit aliquam fermentum. Nam ut blandit nisi.",
						RoomTypeName ="Suite",
						RoomTypeImageUrl = "http://images.wikia.com/twilightsaga/images/2/2f/Mila-Kunis-1.jpg",
						RoomTypeId = roomTypeId 
					}
				}.TransformText()+"</dummy>").Root.Elements();
				room.Add(roomXml);
			}

			//@event.CurrentView.Clear();
			//@event.CurrentView.Append(xml.ToString());
		}
	}
}