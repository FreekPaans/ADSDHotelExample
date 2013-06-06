using Infrastructure.Messaging;
using ITOps.PaymentProvider.Commands;
using ITOps.PaymentProvider.Contracts.AcquireHoldForReservationCancellationFeeContracts;
using ITOps.PaymentProvider.Contracts.Events;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITOps.PaymentProvider {
	public class Handlers : 
			IHandleMessages<AcquireHoldForReservationCancellationFee>,
			IHandleMessages<ReleaseHoldForReservationCancellationFee>,
			IHandleMessages<AcquireHoldForFullAmount>
		{
		readonly IProvideBillingData _billingDataProvider;
		readonly IProvideReservationData _reservationDataProvider;
		readonly IProvidePricingData _pricingDataProvider;
		readonly IEventBus _eventBus;
		public Handlers(IProvidePricingData pricingDataProvider, IProvideReservationData reservationDataProvider, IProvideBillingData billingDataProvider, IEventBus eventBus) {
			_pricingDataProvider = pricingDataProvider;
			_reservationDataProvider = reservationDataProvider;
			_billingDataProvider = billingDataProvider;
			_eventBus = eventBus;
		}
		//TODO: audit interactions with payment provider 

		public void Handle(AcquireHoldForReservationCancellationFee cmd) {
			
			var reservationData = _reservationDataProvider.GetReservationData(cmd.ReservationId);
			var pricingInfo = _pricingDataProvider.GetPricingData(reservationData.RoomTypeId,reservationData.ReservedAt, reservationData.Arrival,reservationData.Checkout);
			var billingData= _billingDataProvider.GetBillingData(cmd.ReservationId);

			var result = MessageBox.Show(FormatDetails(pricingInfo.CancellationFee,billingData), "Acquire hold?", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

			if(result == DialogResult.Yes) {
				_eventBus.Publish(new CancellationFeeHoldAcquiredFromCreditCard { ReservationId = cmd.ReservationId });
			}
			else {
				_eventBus.Publish(new CancellationFeeHoldDeniedFromCreditCard { ReservationId = cmd.ReservationId });
			}
		}

		private string FormatDetails(decimal amount,BillingData billingData) {
			return string.Format("Name: {0}\nAmount:{1}\nCredit card nr: {2}\nCredit card expiration: {3}", billingData.Name, amount,billingData.CreditCardNumber, billingData.CreditCardExpiration);
		}

		public void Handle(ReleaseHoldForReservationCancellationFee cmd) {
			MessageBox.Show(string.Format("Hold for {0} released", cmd.ReservationId),"Info", MessageBoxButtons.OK,MessageBoxIcon.Information);
		}

		public void Handle(AcquireHoldForFullAmount cmd) {
			var reservationData = _reservationDataProvider.GetReservationData(cmd.ReservationId);
			var pricingInfo = _pricingDataProvider.GetPricingData(reservationData.RoomTypeId,reservationData.ReservedAt,reservationData.Arrival,reservationData.Checkout);
			var billingData= _billingDataProvider.GetBillingData(cmd.ReservationId);

			var result = MessageBox.Show(FormatDetails(pricingInfo.FullAmount,billingData),"Acquire hold?",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

			if(result == DialogResult.Yes) {
				_eventBus.Publish(new FullAmountHoldAcquiredFromCreditCard { ReservationId = cmd.ReservationId });
			}
			else {
				_eventBus.Publish(new FullAmountHoldDeniedFromCreditCard { ReservationId = cmd.ReservationId });
			}
		}
	}
}
