using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOps.PaymentProvider.Contracts.AcquireHoldForReservationCancellationFeeContracts {
	public interface IProvideBillingData {
		BillingData GetBillingData(Guid reservationId);
	}

	public class BillingData {
		public string Name{get;set;}
		public string CreditCardNumber{get;set;}
		public DateTime CreditCardExpiration{get;set;}
	}
}
