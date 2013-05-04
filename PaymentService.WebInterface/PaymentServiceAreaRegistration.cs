using System.Web.Mvc;

namespace PaymentService.WebInterface {
	public class PaymentServiceAreaRegistration:AreaRegistration {
		public override string AreaName {
			get {
				return "PaymentService";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context) {
			context.MapRoute(
				"PaymentService_default",
				"PaymentService/{controller}/{action}/{id}",
				new { action = "Index",id = UrlParameter.Optional }
			);
		}
	}
}
