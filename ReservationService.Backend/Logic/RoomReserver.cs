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

			var currentReservations = _context.DayReservations.Where(d => d.Day>=from && d.Day<checkout).ToArray().
				ToDictionary(r => r.Day,r => JsonConvert.DeserializeObject<Dictionary<Guid,int>>(r.ReservationData));

			foreach(var day in currentReservations.Keys) {
				days[day] = currentReservations[day];
			}
			return days;
		}

		public void ReserveRoom(Guid roomType, DateTime from, DateTime checkout) {
			var rooms = _context.DayReservations.Where(d=>d.Day>=from && d.Day<=checkout).ToArray().ToDictionary(d=>d.Day,d=>d);
			
			for(var iterator = from.Date;iterator<checkout;iterator = iterator.AddDays(1)) {
				if(!rooms.ContainsKey(iterator)) {
					rooms[iterator] = new DAL.Models.DayReservations { Day = iterator };
					_context.DayReservations.Add(rooms[iterator]);
				}
				ReserveRoom(rooms[iterator], roomType);
			}
			_context.SaveChanges();
		}

		private void ReserveRoom(DAL.Models.DayReservations dayReservations,Guid roomType) {
			Dictionary<Guid, int> reservedRooms;

			if(dayReservations.ReservationData==null) {
				reservedRooms = new Dictionary<Guid,int>(_roomConfiguration);
			}
			else {
				reservedRooms = JsonConvert.DeserializeObject<Dictionary<Guid,int>>(dayReservations.ReservationData);
			}

			if(reservedRooms[roomType]==0) {
				throw new RoomTypeNotAvailableException();
			}
			reservedRooms[roomType]--;

			dayReservations.ReservationData = JsonConvert.SerializeObject(reservedRooms);
		}
	}
}
