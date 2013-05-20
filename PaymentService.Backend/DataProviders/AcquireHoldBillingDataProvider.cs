using ITOps.PaymentProvider.Contracts.AcquireHoldForReservationCancellationFeeContracts;
using PaymentService.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Backend.DataProviders {
	class AcquireHoldBillingDataProvider : IProvideBillingData {
		readonly PaymentDataContext _context;
		public AcquireHoldBillingDataProvider(PaymentDataContext context) {
			_context = context;
		}
		public BillingData GetBillingData(Guid reservationId) {
			var reservation = _context.BillingData.Find(reservationId);
			return new BillingData { 
				Name = string.Format("{0} {1}",reservation.Firstname, reservation.Lastname),
				CreditCardNumber = reservation.CreditCardNumber,
				CreditCardExpiration = reservation.CreditCardExpiration
			};
		}
	}
}
