using CustomerWebsite.Events;
using CustomerWebsite.WebInterface.ViewModels;
using ReservationService.Contracts.Events.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.XPath;

namespace CustomerWebsite.WebInterface.Controllers {
	public class ReservationController:ADSDControllerBase{
		readonly ReservationSummaryViewModel.Provider _summaryProvider;

		public ReservationController(ReservationSummaryViewModel.Provider summaryProvider) {
			_summaryProvider = summaryProvider;
		}

		[HttpPost]
		public ActionResult Start(Guid roomTypeId, DateTime from,DateTime till ) {
			var id = Guid.NewGuid();
			ProcessingContext.Dispatch(new StartingNewReservation { 
				ReservationId = id,
				RoomTypeId = roomTypeId,
				From = from,
				Till = till

			}  );
			return RedirectToAction("ObtainDetails", new { reservationId = id});
		}

		public ActionResult ObtainDetails(Guid reservationId) {
		
			ProcessingContext.Dispatch(new ObtainingReservationDetails { 
				ReservationId = reservationId,
				SubmitUrl = string.Format("/Reservation/Summary?reservationId={0}",reservationId)
			});
			return View();
		}

		public ActionResult Summary(Guid reservationId) {
			//var reservationSummary = new ShowingReservationSummary {
			//	ReservationId = reservationId
			//};

			//ProcessingContext.Dispatch(reservationSummary);

			//EnrichReservationInformation(reservationSummary);

			return View(_summaryProvider.GetSummary(reservationId));
		}

		//private void EnrichReservationInformation(ShowingReservationSummary reservationSummary) {
		//	var reservationDetails = XDocument.Parse(reservationSummary.ReservationInformation);

		//	GetRoomTypeName(reservationDetails);

		//	AddPricingToTable(reservationSummary,reservationDetails);

		//	reservationSummary.ReservationInformation = reservationDetails.ToString();
		//}

		//private static void AddPricingToTable(ShowingReservationSummary reservationSummary,XDocument reservationDetails) {
		//	var reservationTable = reservationDetails.XPathSelectElement("//table");
		//	reservationTable.Add(XElement.Parse(string.Format("<tr><td>Total Price</td><td>{0}</td></tr>",reservationSummary.PricingInformation)));
		//}

		//private void GetRoomTypeName(XDocument reservationDetails) {
		//	var roomTypeEl = reservationDetails.XPathSelectElement("//*[@class='roomtype_id']");
		//	var roomTypeId = Guid.Parse(roomTypeEl.Attribute("id").Value);

		//	ProcessingContext.Dispatch(new ReservationSummaryRoomTypeIdAvailable { RoomTypeId = roomTypeId,Element = roomTypeEl });
		//}
	}
}
