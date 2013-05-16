using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerWebsite.Contracts.ReservationSummary {
	public interface IProvidePricingDetails {
		PricingDetails GetPricingDetails(Guid reservationId);
	}

	public class PricingDetails {
		public string FormattedTotalPrice{get;set;}
	}
}
