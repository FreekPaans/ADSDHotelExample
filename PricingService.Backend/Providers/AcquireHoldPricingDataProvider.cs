using ITOps.PaymentProvider.Contracts.AcquireHoldForReservationCancellationFeeContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingService.Backend.Providers {
	class AcquireHoldPricingDataProvider : IProvidePricingData{
		public PricingData GetPricingData(Guid roomTypeId,DateTime bookedAt,DateTime arrival,DateTime checkout) {
			return new PricingData { CancellationFee =100M, FullAmount = 700M };
		}
	}
}
