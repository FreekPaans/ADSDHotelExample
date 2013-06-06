using Infrastructure.Dates;
using Infrastructure.Messaging;
using Newtonsoft.Json;
using ReservationService.Backend.DAL;
using ReservationService.Backend.DAL.Models;
using ReservationService.Contracts.Events.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Backend.Logic {
	public class RoomReserver {
		readonly ReservationDataContext _context;
		readonly Dictionary<Guid,int> _roomConfiguration;
		readonly IEventBus _eventBus;

		public RoomReserver(ReservationDataContext context, IEventBus eventBus, Dictionary<Guid, int> roomConfiguration) {
			_context = context;
			_roomConfiguration = roomConfiguration;
			_eventBus = eventBus;
		}

		public ICollection<Guid> CalculateFreeRoomTypes(DateTime from,DateTime to) {
			var available = GetAvailableRoomsPerDay(from,to);

			return _roomConfiguration.Keys.Where(roomTypeId=>available.Values.Count(p=>p[roomTypeId]==0)==0).ToArray();
		}

		Dictionary<DateTime,Dictionary<Guid,int>> GetAvailableRoomsPerDay(DateTime from,DateTime checkout) {
			var days = Enumerable.Range(0,(int)(checkout-from).TotalDays+1).Select(i => from.Date.AddDays(i)).ToDictionary(d => d,d => new Dictionary<Guid,int>(_roomConfiguration));

			var roomOccupancy = _context.DayReservations.Where(d => d.Day>=from && d.Day<checkout).ToArray();
				//ToDictionary(r => r.Day,r => JsonConvert.DeserializeObject<Dictionary<Guid,int>>(r.ReservationData));
				
			foreach(var roomTypeDayOccupancy in roomOccupancy) {
				//days[day] = currentReservations[day];
				days[roomTypeDayOccupancy.Day][roomTypeDayOccupancy.RoomTypeId] = roomTypeDayOccupancy.AvailableRooms;
			}
			return days;
		}

		public void ReserveRoom(Guid reservationId, Guid roomTypeId, DateTime from, DateTime checkout) {
			var rooms = _context.DayReservations.Where(d=>d.Day>=from && d.Day<=checkout && d.RoomTypeId==roomTypeId).ToArray().ToDictionary(d=>d.Day,d=>d);

			try {
				for(var iterator = from.Date;iterator<checkout;iterator = iterator.AddDays(1)) {
					if(!rooms.ContainsKey(iterator)) {
						rooms[iterator] = new DAL.Models.DayReservations { Day = iterator, RoomTypeId = roomTypeId, AvailableRooms = _roomConfiguration[roomTypeId] };
						_context.DayReservations.Add(rooms[iterator]);
					}
					ReserveRoom(rooms[iterator], roomTypeId);
				}

				_context.ReservationsWithAcquiredRoom.Add(new ReservationWithAcquiredRoom { ReservationId = reservationId,RoomTypeId = roomTypeId,Arrival = from,Checkout = checkout });

				_context.SaveChanges();

				_eventBus.Publish(new RoomsForReservationAcquired { ReservationId = reservationId });

				
				//TODO: send timeout for cancellation fee
			}
			catch(RoomTypeNotAvailableException e) {
				//TODO: handle this case
				throw;
			}
		}

		private void ReserveRoom(DAL.Models.DayReservations dayReservations,Guid roomType) {
			if(dayReservations.AvailableRooms==0) {
				throw new RoomTypeNotAvailableException();
			}
			dayReservations.AvailableRooms--;
		}

		void ReleaseRooms(Guid reservationId) {
			var reservation = _context.ReservationsWithAcquiredRoom.Find(reservationId);
			_context.ReservationsWithAcquiredRoom.Remove(reservation);

			foreach(var dayRoomHold in _context.DayReservations.Where(dr => dr.Day>=reservation.Arrival && dr.Day<reservation.Checkout && dr.RoomTypeId == reservation.RoomTypeId)) {
				dayRoomHold.AvailableRooms++;
			}
			_context.SaveChanges();
		}

		public void CancellationFeeHoldAcquired(Guid reservationId) {
			var reservation = _context.ReservationsWithAcquiredRoom.Find(reservationId);
			_eventBus.Publish(new ReservationAccepted { 
				ReservationId = reservationId ,
				Arrival = reservation.Arrival,
				Checkout= reservation.Checkout,
				RoomTypeid = reservation.RoomTypeId
			});
		}

		public void CancellationFeeHoldDenied(Guid reservationId) {
			ReleaseRooms(reservationId);
		}

		public bool CanCheckin(Guid reservationId, out CannotCheckinReason reason) {
			reason = CannotCheckinReason.NotApplicable;

			var reservation = _context.ReservationsWithAcquiredRoom.Find(reservationId);

			if(reservation==null) {
				return CheckinFailed(reservation, out reason);
			}

			if(!IsInCheckinPeriod(reservation)) {
				return CheckinFailed(reservation, out reason);
			}

			return true;

			
		}

		private bool CheckinFailed(ReservationWithAcquiredRoom reservation,out CannotCheckinReason reason) {
			if(reservation==null) {
				reason = CannotCheckinReason.ReservationNotFound;
				return false;
			}
			if(DateProvider.Now.Date < reservation.Arrival.Date) {
				reason = CannotCheckinReason.NotYetAtCheckinDate;
			}
			else {
				reason = CannotCheckinReason.NotYetAtCheckinTime;
			}
			return false;
		}

		public readonly static TimeSpan CheckinTime = TimeSpan.FromHours(12);

		private bool IsInCheckinPeriod(ReservationWithAcquiredRoom reservation) {
			return DateProvider.Now >= reservation.Arrival.Add(CheckinTime);
		}

		public enum CannotCheckinReason {
			NotApplicable,
			ReservationNotFound,
			NotYetAtCheckinDate,
			NotYetAtCheckinTime
		}

		public bool CanCheckin(Guid reservationId) {
			CannotCheckinReason reason;
			return CanCheckin(reservationId,out reason);
		}

		public void StartCheckIn(Guid reservationId) {
			if(!CanCheckin(reservationId)) {
				throw new InvalidOperationException("Cannot checkin yet");
			}
			_eventBus.Publish(new GuestArrived { ReservationId = reservationId});
		}
	}
}
