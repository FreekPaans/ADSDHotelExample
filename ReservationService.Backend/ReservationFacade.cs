using Newtonsoft.Json;
using ReservationService.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Backend {
	public class ReservationFacade {
		readonly ReservationDataContext _context;

		public ReservationFacade(ReservationDataContext context) {
			_context = context;
		}

		static readonly Dictionary<Guid, int> DefaultAvailableRooms = new Dictionary<Guid,int> {
			{ Guid.Parse("3CC3A362-D802-46E6-A27D-22711618AE2D"), 100 },
			{ Guid.Parse("267658E6-3498-4EEC-B269-616FBD07FE9B"), 50 },
			{ Guid.Parse("D9156B38-843E-43C3-BC82-7E4558BD1328"), 1 },
		};

		public ICollection<Guid> GetAvailableRoomTypes(DateTime from,DateTime to) {
			var days = Enumerable.Range(0,(int)(to-from).TotalDays+1).Select(i=>from.Date.AddDays(i)).ToDictionary(d=>d,d=>new Dictionary<Guid,int>(DefaultAvailableRooms));

			var currentReservations = _context.DayReservations.Where(d=>d.Day>=from && d.Day<=to).ToArray().
				ToDictionary(r=>r.Day,r=>JsonConvert.DeserializeObject<Dictionary<Guid,int>>(r.ReservationData));
			
			foreach(var day in currentReservations.Keys) {
				days[day] = currentReservations[day];
			}

			return DefaultAvailableRooms.Keys.Where(roomTypeId=>days.Values.Count(p=>p[roomTypeId]==0)==0).ToArray();
		}
	}
}
