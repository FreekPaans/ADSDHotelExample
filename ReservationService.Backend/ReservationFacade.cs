using Newtonsoft.Json;
using ReservationService.Backend.DAL;
using ReservationService.Backend.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Backend {
	public class ReservationFacade {
		readonly RoomReserver _calculator;
		readonly ReservationDataContext _context;
		

		public ReservationFacade(RoomReserver calculator, ReservationDataContext context) {
			_calculator = calculator;
			_context = context;
		}

		public ICollection<Guid> GetAvailableRoomTypes(DateTime from,DateTime to) {
			return _calculator.CalculateFreeRoomTypes(from,to);
		}

		public bool ReservationExists(Guid reservationId) {
			return _context.Reservations.Any(r=>r.ReservationId == reservationId);
		}
	}
}
