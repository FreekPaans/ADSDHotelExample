using Newtonsoft.Json;
using ReservationService.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Backend.Logic {
	public class RoomReserver {
		readonly ReservationDataContext _context;
		readonly Dictionary<Guid,int> _roomConfiguration;

		public RoomReserver(ReservationDataContext context, Dictionary<Guid, int> roomConfiguration) {
			_context = context;
			_roomConfiguration = roomConfiguration;
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

		public void ReserveRoom(Guid roomTypeId, DateTime from, DateTime checkout) {
			var rooms = _context.DayReservations.Where(d=>d.Day>=from && d.Day<=checkout && d.RoomTypeId==roomTypeId).ToArray().ToDictionary(d=>d.Day,d=>d);
			
			for(var iterator = from.Date;iterator<checkout;iterator = iterator.AddDays(1)) {
				if(!rooms.ContainsKey(iterator)) {
					rooms[iterator] = new DAL.Models.DayReservations { Day = iterator, RoomTypeId = roomTypeId, AvailableRooms = _roomConfiguration[roomTypeId] };
					_context.DayReservations.Add(rooms[iterator]);
				}
				ReserveRoom(rooms[iterator], roomTypeId);
			}
			_context.SaveChanges();
		}

		private void ReserveRoom(DAL.Models.DayReservations dayReservations,Guid roomType) {
			
			if(dayReservations.AvailableRooms==0) {
				throw new RoomTypeNotAvailableException();
			}
			dayReservations.AvailableRooms--;
		}

		public void ReleaseRooms(Guid roomTypeId,DateTime from,DateTime to) {
			foreach(var dayRoomHold in _context.DayReservations.Where(dr => dr.Day>=from && dr.Day<to && dr.RoomTypeId == roomTypeId)) {
				dayRoomHold.AvailableRooms++;
			}
			_context.SaveChanges();
		}
	}
}
