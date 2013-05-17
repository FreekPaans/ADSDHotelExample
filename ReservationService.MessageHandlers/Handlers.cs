using NServiceBus;
using ReservationService.Backend.Commands;
using ReservationService.Backend.DAL;
using ReservationService.Backend.DAL.Models;
using ReservationService.Backend.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReservationService.MessageHandlers {
	//TODO: remove dependency on NSB (or maybe add in other parts)
	public class Handlers : IHandleMessages<StartReservation>, IHandleMessages<CommitReservation>{
		readonly ReservationDataContext _context;
		readonly RoomReserver _roomReserver;
		
		public Handlers(ReservationDataContext context, RoomReserver roomReserver) {
			_context = context;
			_roomReserver = roomReserver;
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
				RoomTypeId = message.RoomTypeId		
			});
			_context.SaveChanges();
		}

		public void Handle(CommitReservation message) {
			var reservation = _context.Reservations.Find(message.ReservationId);
			
			reservation.Status = Reservation.Committed;

			_roomReserver.ReserveRoom(reservation.RoomTypeId,reservation.From,reservation.To);
		}
	}
}
