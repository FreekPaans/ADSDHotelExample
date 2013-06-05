using Infrastructure.HTTP.ProcessingPipeline;
using ReservationService.Contracts.Events.UI;
using ReservationService.WebInterface.Models;
using ReservationService.WebInterface.ViewModels;
using ReservationService.WebInterface.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Infrastructure.HTTP.Helpers;
using Infrastructure.Messaging;
using CustomerWebsite.Contracts.Events;
using Infrastructure.HTTP.Session;
using ReservationService.Backend.Commands;
using ReservationService.Backend;

namespace ReservationService.WebInterface.Handlers {
	public class HttpRequestHandler  : IHandleHttpRequests, OnDemandViewRenderer,
			IHandleHttpProcessingEvents<SearchingForRooms>, 
			IHandleHttpProcessingEvents<StartingNewReservation>,
			IHandleHttpProcessingEvents<ObtainingReservationDetails> {
		readonly ICommandBus _commandBus;
		readonly ISessionStorage _sessionStorage;
		readonly ReservationFacade _facade;

		private SearchParameters _searchParams;
		
		public HttpRequestHandler(ICommandBus commandBus, ISessionStorage sessionStorage, ReservationFacade facade) {
			_commandBus = commandBus;
			_sessionStorage = sessionStorage;
			_facade = facade;
		}

		public void HandleHttpRequest(HttpProcessingPipelineContext context) {
			context.WriteView("ReservationService_JSFacade", @"<script src=""/Scripts/Services/ReservationService/Facade.js"" type=""text/javascript""></script>");
		}

		const string SearchBoxViewName = "Reservations_Searchbox";
		

		public void DrawViewOnDemand(HttpProcessingPipelineContext context, string viewName) {
			if(viewName == SearchBoxViewName) {
				ShowSearchBox(context);
			}
		}

		private void ShowSearchBox(HttpProcessingPipelineContext context) {
			context.WriteView(SearchBoxViewName,()=>new SearchRoomsBox { 
				Model =_searchParams
			}.TransformText());
		}

		public void Handle(HttpProcessingPipelineContext context, SearchingForRooms @event) {
			_searchParams= new SearchParameters { From = @event.From, Till = @event.Till};
			
			//var roomTypeIds = new [] { Guid.NewGuid(), Guid.NewGuid()};

			//var view = ;
			

			var roomTypeIds = _facade.GetAvailableRoomTypes(@event.From,@event.Till);
			
			context.Dispatch(new RoomTypeIDsAvailable { 
				RoomTypeIds = roomTypeIds
			} );

			context.WriteView("Reservations_SearchResults", new SearchResultsView { 
				Model = new SearchResultsViewModel { 
					RoomTypeIds = roomTypeIds,
					From = @event.From,
					Till = @event.Till
				}
			}.TransformText());
		}

		public void Handle(HttpProcessingPipelineContext context,StartingNewReservation @event) {
			var cmd = new StartReservation { 
				ReservationId = @event.ReservationId,
				From = @event.From,
				Till = @event.Till,
				RoomTypeId =@event.RoomTypeId,
				DateReserved = DateTime.Now
			};
			_sessionStorage[@event.ReservationId] = cmd;
			_commandBus.Send(cmd);
		}

		public void Handle(HttpProcessingPipelineContext context,ObtainingReservationDetails @event) {
			context.WriteView("Reservations_DetailsForm",() => new ReservationDetailsForm { ReservationId = @event.ReservationId }.TransformText());
		}
	}
}