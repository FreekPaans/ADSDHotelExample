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
using ReservationService.Commands;
using CustomerWebsite.Events;
using System.Xml.XPath;

namespace ReservationService.WebInterface.Handlers {
	public class HttpRequestHandler  : IHandleHttpRequests, 
			IHandleHttpProcessingEvents<SearchingForRooms>, 
			IHandleHttpProcessingEvents<StartingNewReservation>,
			IHandleHttpProcessingEvents<ObtainingReservationDetails>,
			IHandleHttpProcessingEvents<ShowingReservationSummary>
			{
		readonly ICommandBus _commandBus;
		private SearchParameters _searchParams;
		
		public HttpRequestHandler(ICommandBus commandBus ) {
			_commandBus = commandBus;
		}

		public void HandleHttpRequest(HttpProcessingPipelineContext context) {
			ShowSearchBox(context);
		}
	
		private void ShowSearchBox(HttpProcessingPipelineContext context) {
			context.WriteView("Reservations_Searchbox",()=>new SearchRoomsBox { 
				Model =_searchParams
			}.TransformText());
		}

		public void Handle(HttpProcessingPipelineContext context, SearchingForRooms @event) {
			_searchParams= new SearchParameters { From = @event.From, Till = @event.Till};
			
			var roomTypeIds = new [] { Guid.NewGuid(), Guid.NewGuid()};

			var view = XDocument.Parse(new SearchResultsView { 
				Model = new SearchResultsViewModel { 
					RoomTypeIds = roomTypeIds,
					From = @event.From,
					Till = @event.Till
				}
			}.TransformText()).Root;
			
			
			context.Dispatch(new RoomTypeIDsAvailable { 
				RoomTypeIds = roomTypeIds,
				CurrentView = view
			} );

			context.WriteView("Reservations_SearchResults", ()=>view.ToString());
		}

		public void Handle(HttpProcessingPipelineContext context,StartingNewReservation @event) {
			
		}

		public void Handle(HttpProcessingPipelineContext context,ObtainingReservationDetails @event) {
			var form= XElement.Parse(new ReservationDetailsForm { Model = @event}.TransformText());
			context.Dispatch(new RenderingObtainReservationDetailsForm { Form = form.XPathSelectElement("//form"), ReservationId = @event.ReservationId});
			context.WriteView("Reservations_DetailsForm",()=>form.ToString() );
		}

		public void Handle(HttpProcessingPipelineContext context,ShowingReservationSummary @event) {
			@event.ReservationInformation = new ReservationSummaryInformationView {}.TransformText();
		}
	}
}