using Infrastructure.Messaging;
using NServiceBus;
using Occupancy.Backend.Commands;
using Occupancy.Backend.DAL;
using Occupancy.Contracts.Events;
using PaymentService.Contracts.Events;
using ReservationService.Contracts.Events.Business;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occupancy.Backend {
	public class Handlers : 
			IHandleMessages<CheckInToRoom>,
			IHandleMessages<ReservationAccepted>, 
			IHandleMessages<GuestArrived>, 
			IHandleMessages<FullAmountHoldDenied>,
			IHandleMessages<FullAmountHoldAcquired>
		{
		readonly DataContext _context;
		readonly IEventBus _eventBus;

		public Handlers(DataContext context, IEventBus eventBus) {
			_context = context;
			_eventBus = eventBus;
		}
		public void Handle(CheckInToRoom cmd) {
			var reservation = _context.Reservations.Find(cmd.ReservationId);

			if(!reservation.GuestArrived) {
				throw new InvalidOperationException("Trying to assign occupancy while guests haven't arrived");
			}

			if(reservation.RoomAcquired) {
				//be idempotent
				return;
			}
			
			//TODO: check room number is valid

			for(var iterator = reservation.Arrival;iterator<reservation.Checkout;iterator= iterator.AddDays(1)) {
				_context.RoomOccupancy.Add(new DAL.Models.RoomOccupancy {
					Date = iterator, RoomNumber = cmd.RoomNumber, ReservationId = cmd.ReservationId
				});
			}

			try {
				_context.SaveChanges();
			}
			catch(DbUpdateException ex) {
				//TODO: test this flow
				if(((SqlException)ex.InnerException.InnerException).Number!=2627) {
					throw;
				}
				_eventBus.Publish(new CheckInToRoomFailedBecauseRoomIsAlreadyOccupied {
					ReservationId = cmd.ReservationId
				});
				return;
			}

			reservation.RoomAcquired = true;

			_context.SaveChanges();

			_eventBus.Publish(new ReservationRoomOccupied {
				ReservationId = reservation.ReservationId,
				Tentative = true
			});


			//todo: set timeout to release rooms when payment doesn't arrive
		}
		
		public void Handle(ReservationAccepted @event) {
			_context.Reservations.Add(new DAL.Models.Reservation {
				Arrival = @event.Arrival,
				Checkout = @event.Checkout,
				ReservationId = @event.ReservationId
			});
			_context.SaveChanges();
		}

		public void Handle(GuestArrived @event) {
			var reservation = _context.Reservations.Find(@event.ReservationId);

			reservation.GuestArrived = true;
			_context.SaveChanges();
		}

		public void Handle(FullAmountHoldDenied @event) {
			var reservation = _context.Reservations.Find(@event.ReservationId);

			reservation.RoomAcquired = false;
			reservation.GuestArrived = false;
			foreach(var roomOccupancy in _context.RoomOccupancy.Where(ro=>ro.ReservationId == reservation.ReservationId)) {
				_context.RoomOccupancy.Remove(roomOccupancy);
			}
			_context.SaveChanges();

			_eventBus.Publish(new ReservationRoomReleased { ReservationId = reservation.ReservationId, Reason = "CouldNotAcquireHold" });
		}

		public void Handle(FullAmountHoldAcquired @event) {
			_eventBus.Publish(new ReservationRoomOccupied { ReservationId = @event.ReservationId, Tentative = false});
		}
	}
}
