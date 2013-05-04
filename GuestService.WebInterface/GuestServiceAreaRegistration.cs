using System.Web.Mvc;

namespace GuestService.WebInterface {
	public class GuestServiceAreaRegistration:AreaRegistration {
		public override string AreaName {
			get {
				return "GuestService";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context) {
			context.MapRoute(
				"GuestService_default",
				"GuestService/{controller}/{action}/{id}",
				new { action = "Index",id = UrlParameter.Optional }
			);
		}
	}
}
