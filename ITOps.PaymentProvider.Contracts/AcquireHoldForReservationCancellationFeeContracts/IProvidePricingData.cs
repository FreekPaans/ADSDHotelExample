using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOps.PaymentProvider.Contracts.AcquireHoldForReservationCancellationFeeContracts {
	public interface IProvidePricingData {
		PricingData GetPricingData(Guid roomTypeId, DateTime bookedAt, DateTime arrival, DateTime checkout);
	}
	public class PricingData{
		public decimal CancellationFee{get;set;}

		public decimal FullAmount { get; set; }
	}
}
