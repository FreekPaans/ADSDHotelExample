using CustomerWebsite.Contracts.ReservationSummary;
using PaymentService.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentService.Backend.DataProviders {
	class PaymentDetailsProvider:IProvidePaymentDetails {
		readonly PaymentDataContext _context;
		public PaymentDetailsProvider(PaymentDataContext context) {
			_context = context;
		}
		public PaymentDetails GetPaymentDetails(Guid reservationId) {
			var data = _context.BillingData.Find(reservationId);


			return new PaymentDetails { BillingAddress = 
				FormatAddress(data),
				BillingName = string.Format("{0} {1}", data.Firstname, data.Lastname),
				CreditCardNumber = ObfuscateCreditcardNumber(data.CreditCardNumber)
			};
		}

		const int ShowCreditcardDigits = 4;

		private static string ObfuscateCreditcardNumber(string creditCardNumber) {
			if(creditCardNumber.Length<ShowCreditcardDigits) {
				return creditCardNumber;
			}

			var sb = new StringBuilder();
			sb.Append(Enumerable.Range(0,creditCardNumber.Length-ShowCreditcardDigits).Select(i=>'*').ToArray());
			sb.Append(creditCardNumber.Substring(creditCardNumber.Length-ShowCreditcardDigits,ShowCreditcardDigits));
			InsertSpacesAtOffset(sb,4);
			return sb.ToString();
		}

		private static void InsertSpacesAtOffset(StringBuilder sb, int offset) {
			var len = sb.Length;
			for(var i=len-offset;i>0;i-=offset) {
				sb.Insert(i," ");
			}
		}

		private string FormatAddress(DAL.Models.BillingData data) {
			return string.Join("\n", new [] { data.StreetName, data.PostalCode,data.City }.Where(s=>!string.IsNullOrEmpty(s)).ToArray());
		}
	}
}
