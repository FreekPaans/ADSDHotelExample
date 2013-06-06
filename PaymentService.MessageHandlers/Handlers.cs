using Infrastructure.Messaging;
using ITOps.PaymentProvider.Commands;
using ITOps.PaymentProvider.Contracts.Events;
using NServiceBus;
using Occupancy.Contracts.Events;
using PaymentService.Backend;
using ReservationService.Contracts.Events.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.MessageHandlers {
	public class Handlers : 
			IHandleMessages<RoomsForReservationAcquired>,
			IHandleMessages<CancellationFeeHoldAcquiredFromCreditCard>, 
			IHandleMessages<CancellationFeeHoldDeniedFromCreditCard>,
			IHandleMessages<GuestArrived>,
			IHandleMessages<ReservationRoomOccupied>,
			IHandleMessages<FullAmountHoldAcquiredFromCreditCard>,
			IHandleMessages<FullAmountHoldDeniedFromCreditCard>
	{
		readonly PaymentFacade _facade;
		//readonly PaymentFacade _facade;

		public Handlers(PaymentFacade facade) {
			_facade = facade;
		}
		public void Handle(RoomsForReservationAcquired @event) {
			_facade.Handle(@event);
		}

		public void Handle(CancellationFeeHoldAcquiredFromCreditCard @event) {
			_facade.Handle(@event);
		}

		public void Handle(CancellationFeeHoldDeniedFromCreditCard @event) {
			_facade.Handle(@event);
		}

		public void Handle(GuestArrived @event) {
			_facade.Handle(@event);
		}

		public void Handle(ReservationRoomOccupied @event) {
			_facade.Handle(@event);
		}

		public void Handle(FullAmountHoldDeniedFromCreditCard @event) {
			_facade.Handle(@event);
		}

		public void Handle(FullAmountHoldAcquiredFromCreditCard @event) {
			_facade.Handle(@event);
		}
	}
}
