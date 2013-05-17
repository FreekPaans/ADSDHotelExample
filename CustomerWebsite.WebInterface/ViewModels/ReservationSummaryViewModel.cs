using CustomerWebsite.Contracts;
using CustomerWebsite.Contracts.ReservationSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerWebsite.WebInterface.ViewModels {
	public class ReservationSummaryViewModel {
		public GuestDetails GuestDetails{get;set;}
		public ReservationDetails ReservationDetails{get;set;}
		public Guid ReservationId{get;set;}
		public RoomTypeDetails RoomTypeDetails { get; set; }
		public PricingDetails PricingDetails { get; set; }
		public PaymentDetails PaymentDetails { get; set; }

		public class Provider {
			private IProvideReservationDetails _reservationDetailsProvider;
			private IProvideRoomTypeDetails _roomTypeDetailsProvider;
			private IProvidePricingDetails _pricingDetailsProvider;
			private IProvidePaymentDetails _paymentDetailsProvider;
			private IProvideGuestDetails _guestDetailsProvider;

			public Provider(IProvideReservationDetails reservationDetailsProvider, 
						IProvideRoomTypeDetails roomTypeDetailsProvider,
						IProvidePricingDetails pricingDetailsProvider,
						IProvidePaymentDetails paymentDetailsProvider,
						IProvideGuestDetails guestDetailsProvider
				) {
				_reservationDetailsProvider = reservationDetailsProvider;
				_roomTypeDetailsProvider = roomTypeDetailsProvider;
				_pricingDetailsProvider = pricingDetailsProvider;
				_paymentDetailsProvider = paymentDetailsProvider;
				_guestDetailsProvider = guestDetailsProvider;
			}

			public ReservationSummaryViewModel GetSummary(Guid reservationId) {
				var resDetails = _reservationDetailsProvider.GetReservationDetails(reservationId);
				return new ReservationSummaryViewModel { 
					ReservationId = reservationId,
					ReservationDetails = resDetails,
					RoomTypeDetails = _roomTypeDetailsProvider.GetRoomTypeDetails(resDetails.RoomTypeId),
					PricingDetails = _pricingDetailsProvider.GetPricingDetails(reservationId,resDetails.DateBooked,resDetails.ArrivalDate,resDetails.CheckoutDate),
					PaymentDetails = _paymentDetailsProvider.GetPaymentDetails(reservationId),
					GuestDetails = _guestDetailsProvider.GetGuestDetails(reservationId)
				};
			}
		}
	}
}