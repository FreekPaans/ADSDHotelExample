using Infrastructure.Messaging;
using ITOps.PaymentProvider.Contracts.Events;
using NServiceBus;
using PaymentService.Contracts.Events;
using ReservationService.Backend.Commands;
using ReservationService.Backend.DAL;
using ReservationService.Backend.DAL.Models;
using ReservationService.Contracts.Events.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.MessageHandlers {
	public class ReservationHandlers :	
			IHandleMessages<StartReservation>, 
			IHandleMessages<CommitReservation>, 
			IHandleMessages<CancellationFeeHoldAcquired>, 
			IHandleMessages<CancellationFeeHoldDenied>,
			IHandleMessages<AcquiringHoldForReservationCancellationFee>,
			IHandleMessages<StartCheckIn>
			 {
		readonly ReservationDataContext _context;
		readonly IEventBus _eventBus;
		public ReservationHandlers(ReservationDataContext context, IEventBus eventBus) {
			_context = context;
			_eventBus = eventBus;
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
				FlowStatus = Reservation.FlowStarted,
				ReservedAt = message.DateReserved,
				CancellationFeeStatus = ReservationCancellationFeeStatus.NotApplicable
			});

			_context.SaveChanges();

		}

		public void Handle(CancellationFeeHoldAcquired @event) {
			UpdateReservation(@event.ReservationId,r=>r.CancellationFeeStatus = ReservationCancellationFeeStatus.Acquired);
		}

		public void Handle(CommitReservation cmd) {
			UpdateReservation(cmd.ReservationId,r=>r.FlowStatus = Reservation.Committed);

			_eventBus.Publish(new ReservationCommitted { ReservationId = cmd.ReservationId });
		}

		public void Handle(CancellationFeeHoldDenied @event) {
			UpdateReservation(@event.ReservationId, r=> r.CancellationFeeStatus = ReservationCancellationFeeStatus.Denied);
		}

		public void Handle(AcquiringHoldForReservationCancellationFee @event) {
			UpdateReservation(@event.ReservationId, r=> {
				if(r.CancellationFeeStatus==ReservationCancellationFeeStatus.NotApplicable) {
					r.CancellationFeeStatus = ReservationCancellationFeeStatus.Pending;
				}
			});
		}

		void UpdateReservation(Guid reservationId, Action<Reservation> update) {
			var reservation = _context.Reservations.Find(reservationId);
			update(reservation);
			_context.SaveChanges();
		}

		public void Handle(StartCheckIn cmd) {
			_eventBus.Publish(new GuestArrived { ReservationId = cmd.ReservationId });
		}
	}
}
