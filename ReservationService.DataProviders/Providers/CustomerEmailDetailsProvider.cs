using Infrastructure.Dates;
using ReservationService.Backend.DAL;
using ReservationService.Backend.DAL.Models;
using ReservationService.Backend.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.DataProviders {
	class CustomerEmailDetailsProvider:
			Frontdesk.Contracts.ReservationCheckin.IProvideReservationDetails,
			ITOps.ReservationCustomerEmails.Contracts.IProvideReservationDetails {
		readonly ReservationDataContext _context;
		readonly RoomReserver _roomReserver;
		public CustomerEmailDetailsProvider(ReservationDataContext context, RoomReserver roomReserver) {
			_context = context;
			_roomReserver = roomReserver;
		}
		

		Frontdesk.Contracts.ReservationCheckin.ReservationDetails Frontdesk.Contracts.ReservationCheckin.IProvideReservationDetails.GetReservationDetails(Guid reservationId) {
			return  ((Frontdesk.Contracts.ReservationCheckin.IProvideReservationDetails)this).GetReservationsDetails(new [] {reservationId}).First();
		}

		ICollection<Frontdesk.Contracts.ReservationCheckin.ReservationDetails> Frontdesk.Contracts.ReservationCheckin.IProvideReservationDetails.GetReservationsDetails(ICollection<Guid> reservationIds) {
			var reservations = _context.Reservations.Where(r=>reservationIds.Contains(r.ReservationId)).ToArray();

			var checkins = _roomReserver.CanCheckin(reservationIds).ToDictionary(r=>r.ReservationId, r=>r);

			return reservations.Select(r=>new Frontdesk.Contracts.ReservationCheckin.ReservationDetails {
				ReservationId = r.ReservationId,
				CanCheckin = checkins[r.ReservationId].CanCheckIn,
				CheckinDate = r.From,
				CheckoutDate = r.To,
				RoomTypeId =r.RoomTypeId,
				Status = GetFriendlyStatus(r, checkins[r.ReservationId]),
				FinishedCheckInProcess = r.IsCheckedIn || r.CheckInFailed,
				CheckInFailedReason = r.FailedReason,
				CheckedInSuccesfully = r.IsCheckedIn
			}).ToArray();
		}

		private string GetFriendlyStatus(Backend.DAL.Models.ReservationStatus reservation, ReservationService.Backend.Logic.RoomReserver.CanCheckInInfo canCheckIn) {
			
			if(canCheckIn.CanCheckIn) {
				return "Reservation ready for checkin";
			}

			var reason = canCheckIn.CannotCheckInReason;

			if(reason == RoomReserver.CannotCheckinReason.ReservationNotFound) {
				if(reservation.CancellationFeeStatus == ReservationCancellationFeeStatus.TimedOut) {
					return "Acquiring cancellation fee timed out";
				}

				if(reservation.CancellationFeeStatus == ReservationCancellationFeeStatus.Denied) {
					return "Cancellation fee was denied";
				}

				if(reservation.FlowStatus!=ReservationStatus.Committed) {
					return "Reservation was never finalized";
				}

				//todo implement noshow
				return "Reservation was noshow";
			}


			if(reason == RoomReserver.CannotCheckinReason.NotYetAtCheckinDate) {
				return "Reservation does not start today";
			}
			if(reason==RoomReserver.CannotCheckinReason.NotYetAtCheckinTime) {
				return string.Format("Check in starts at {0}",RoomReserver.CheckinTime);
			}
			
			//TODO: should throw exception here or at least log it
			return "Unknown status";
		}

		ITOps.ReservationCustomerEmails.Contracts.ReservationDetails ITOps.ReservationCustomerEmails.Contracts.IProvideReservationDetails.GetDetails(Guid reservationId) {
			var reservation = _context.Reservations.Find(reservationId);
			return new ITOps.ReservationCustomerEmails.Contracts.ReservationDetails {
				ArrivalDate = reservation.From,
				CheckoutDate = reservation.To
			};
		}


		
	}
}
