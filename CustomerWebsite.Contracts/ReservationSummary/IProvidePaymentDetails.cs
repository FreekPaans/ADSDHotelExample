using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerWebsite.Contracts.ReservationSummary {
	public interface IProvidePaymentDetails {
		PaymentDetails GetPaymentDetails(Guid reservationId);
	}

	public class PaymentDetails {
		public string BillingName{get;set;}
		public string BillingAddress{get;set;}
		public string CreditCardNumber{get;set;}
	}
}
