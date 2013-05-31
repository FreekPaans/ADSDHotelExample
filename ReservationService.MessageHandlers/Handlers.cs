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
	public class Handlers:
		IHandleMessages<StartReservation>,
		IHandleMessages<CommitReservation>,
		IHandleMessages<CancellationFeeHoldAcquired>,
		IHandleMessages<CancellationFeeHoldDenied>,
		IHandleMessages<ReservationAccepted>,
		IHandleMessages<ReservationRejected>
	{
		readonly ReservationDataContext _context;
		readonly RoomReserver _roomReserver;
		readonly IEventBus _eventBus;
		readonly ICommandBus _commandBus;
		
		public Handlers(ReservationDataContext context, RoomReserver roomReserver, IEventBus eventBus, ICommandBus commandBus ) {
			_context = context;
			_roomReserver = roomReserver;
			_eventBus = eventBus;
			_commandBus = commandBus;
		}
		public void Handle(StartReservation message) {
			if(_context.Reservations.Find(message.ReservationId)!=null) {
				//be idempotent
				return;
			}
			_context.Reservations.Add(new Reservation {
				ReservationId = message.ReservationId,
				From = message.From,
				To  = message.Till,
				RoomTypeId = message.RoomTypeId,
				Status = Reservation.Pending,
				ReservedAt = message.DateReserved,
				CancellationFeeStatus = ReservationCancellationFeeStatus.Pending
			});
			
			_context.SaveChanges();
			
		}

		public void Handle(CommitReservation message) {
			var reservation = _context.Reservations.Find(message.ReservationId);
			try {
				_roomReserver.ReserveRoom(reservation.RoomTypeId,reservation.From,reservation.To);

				_eventBus.Publish(new ReservationPlaced { ReservationId = message.ReservationId });

				reservation.Status = Reservation.Committed;
				_context.SaveChanges();

				//TODO: send timeout for cancellation fee
			}
			catch(RoomTypeNotAvailableException e) {
				//TODO: handle this case
				throw;
			}
		}

		public void Handle(CancellationFeeHoldDenied @event) {
			var reservation = _context.Reservations.Find(@event.ReservationId);
			reservation.CancellationFeeStatus = ReservationCancellationFeeStatus.Denied;
			
			_roomReserver.ReleaseRooms(reservation.RoomTypeId,reservation.From,reservation.To);
			
			_context.SaveChanges();

			_eventBus.Publish(new ReservationRejected { ReservationId = @event.ReservationId, Reason = ReservationRejected.BecauseCancellationFeeHoldDenied });
		}

		public void Handle(CancellationFeeHoldAcquired @event) {
			var reservation = _context.Reservations.Find(@event.ReservationId);
			reservation.CancellationFeeStatus = ReservationCancellationFeeStatus.Acquired;
			_context.SaveChanges();

			_eventBus.Publish(new ReservationAccepted { ReservationId = @event.ReservationId });
		}

		public void Handle(ReservationAccepted @event) {
			_commandBus.Send(new SendReservationAcceptedEmail { ReservationId = @event.ReservationId });
		}

		public void Handle(ReservationRejected @event) {
			_commandBus.Send(new SendReservationRejectedEmail { ReservationId = @event.ReservationId, Reason = @event.Reason });
		}
	}
}
