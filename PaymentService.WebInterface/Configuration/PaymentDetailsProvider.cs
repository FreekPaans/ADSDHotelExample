using CustomerWebsite.Contracts.ReservationSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentService.WebInterface.Configuration {
	class PaymentDetailsProvider:IProvidePaymentDetails {
		public PaymentDetails GetPaymentDetails(Guid reservationId) {
			return new PaymentDetails { BillingAddress = "Torenallee 34\n5134 EV Eindhvoen", BillingName = "Frek Paans", CreditCardNumber = "**** **** **** 1234" };
		}
	}
}
