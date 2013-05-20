using Infrastructure.Messaging;
using ITOps.PaymentProvider.Commands;
using ITOps.PaymentProvider.Contracts.Events;
using NServiceBus;
using ReservationService.Contracts.Events.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.MessageHandlers {
	public class Handlers : IHandleMessages<ReservationPlaced>,IHandleMessages<CancellationFeeHoldAcquired>, IHandleMessages<CancellationFeeHoldDenied> {
		readonly ICommandBus _commandBus;
		public Handlers(ICommandBus commandBus) {
			_commandBus = commandBus;
		}
		public void Handle(ReservationPlaced @event) {
			_commandBus.Send(new AcquireHoldForReservationCancellationFee { ReservationId = @event.ReservationId });
		}

		public void Handle(CancellationFeeHoldDenied message) {
			throw new NotImplementedException();
		}

		public void Handle(CancellationFeeHoldAcquired message) {
			throw new NotImplementedException();
		}
	}
}
