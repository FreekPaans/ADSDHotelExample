using CustomerWebsite.Contracts.ReservationSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PricingService.WebInterface.Configuration {
	class PricingDetailsProvider:IProvidePricingDetails {
		public PricingDetails GetPricingDetails(Guid reservationId) {
			return new PricingDetails { FormattedTotalPrice = "$100" };
		}
	}
}
