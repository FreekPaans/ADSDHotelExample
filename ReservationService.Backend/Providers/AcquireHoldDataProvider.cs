using ITOps.PaymentProvider.Contracts.AcquireHoldForReservationCancellationFeeContracts;
using ReservationService.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Backend.Providers {
	public class AcquireHoldDataProvider : IProvideReservationData{
		readonly ReservationDataContext _context;
		public AcquireHoldDataProvider(ReservationDataContext context) {
			_context = context;
		}
		public ReservationData GetReservationData(Guid reservationId) {
			var reservation = _context.Reservations.Find(reservationId);
			return new ReservationData { Arrival = reservation.From,Checkout = reservation.To, RoomTypeId = reservation.RoomTypeId, ReservedAt = reservation.ReservedAt };
		}
	}
}
