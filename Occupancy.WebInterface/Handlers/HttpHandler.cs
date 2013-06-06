using Frontdesk.Contracts.Events;
using Infrastructure.HTTP.ProcessingPipeline;
using Occupancy.WebInterface.ViewModels;
using Occupancy.WebInterface.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Occupancy.WebInterface.Handlers {
	public class HttpHandler : IHandleHttpRequests, IHandleHttpProcessingEvents<StartingCheckIn>{
		public void HandleHttpRequest(HttpProcessingPipelineContext httpContext) {
			
		}

		public void Handle(HttpProcessingPipelineContext context,StartingCheckIn @event) {
			context.WriteView("OccupancyService_NonOccupiedRoomsSelector",RenderNonOccupiedRooms());
		}

		int _currentGuidCount = 0;
		Guid _next = Guid.NewGuid();

		Guid NextGuid {
			get {
				if(_currentGuidCount++%15==0) {
					_next = Guid.NewGuid();
				}
				return _next;
			}
		}

		private string RenderNonOccupiedRooms() {
			var rooms = Enumerable.Range(1,5).Select(r => r*100).SelectMany(r => Enumerable.Range(0,10).Select(i => 1+i+r)).Select(r => new RoomViewModel { RoomNumber = r.ToString(),RoomType =NextGuid }).ToArray();
			rooms[0].Selected=true;
			return new SelectNonOccupiedRooms {
				AvailableRooms = rooms
			}.TransformText();
		}
	}
}