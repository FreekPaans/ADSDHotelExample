using Infrastructure.Dates;
using Infrastructure.Messaging;
using ITOps.ReservationCustomerEmails.Commands;
using NServiceBus;
using PaymentService.Contracts.Events;
using ReservationService.Backend.Commands;
using ReservationService.Backend.DAL;
using ReservationService.Backend.DAL.Models;
using ReservationService.Backend.Logic;
using ReservationService.Contracts.Events.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReservationService.MessageHandlers {
	//TODO: remove dependency on NSB (or maybe add in other parts)
	public class RoomReserverHandlers:
		IHandleMessages<CancellationFeeHoldAcquired>,
		IHandleMessages<CancellationFeeHoldDenied>,
		IHandleMessages<ReservationAccepted>,
		IHandleMessages<ReservationRejected>,
		IHandleMessages<ReservationCommitted>,
		IHandleMessages<StartCheckIn>
	{
		readonly ReservationDataContext _context;
		readonly RoomReserver _roomReserver;
		readonly IEventBus _eventBus;
		readonly ICommandBus _commandBus;
		
		public RoomReserverHandlers(ReservationDataContext context, RoomReserver roomReserver, IEventBus eventBus, ICommandBus commandBus ) {
			_context = context;
			_roomReserver = roomReserver;
			_eventBus = eventBus;
			_commandBus = commandBus;
		}
	
		public void Handle(CancellationFeeHoldDenied @event) {
			_roomReserver.CancellationFeeHoldDenied(@event.ReservationId);
		}

		public void Handle(CancellationFeeHoldAcquired @event) {
			_roomReserver.CancellationFeeHoldAcquired(@event.ReservationId);
		}

		public void Handle(ReservationAccepted @event) {
			_commandBus.Send(new SendReservationAcceptedEmail { ReservationId = @event.ReservationId });
		}

		public void Handle(ReservationRejected @event) {
			_commandBus.Send(new SendReservationRejectedEmail { ReservationId = @event.ReservationId, Reason = @event.Reason });
		}

		public void Handle(ReservationCommitted message) {
			var reservation = _context.Reservations.Find(message.ReservationId);

			_roomReserver.ReserveRoom(reservation.ReservationId,reservation.RoomTypeId,reservation.From,reservation.To);
		}

		public void Handle(StartCheckIn cmd) {
			using(DateProvider.OverrideDateTime(cmd.DateContext)) {
				_roomReserver.StartCheckIn(cmd.ReservationId);
			}
		}
	}
}
