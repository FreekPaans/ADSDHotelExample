using System.Web.Mvc;

namespace ReservationService.WebInterface {
	public class ReservationServiceAreaRegistration:AreaRegistration {
		public override string AreaName {
			get {
				return "ReservationService";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context) {
			context.MapRoute(
				"ReservationService_default",
				"ReservationService/{controller}/{action}/{id}",
				new { action = "Index",id = UrlParameter.Optional }
			);
		}
	}
}
