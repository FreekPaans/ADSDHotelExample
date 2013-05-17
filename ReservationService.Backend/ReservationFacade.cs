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

		public ReservationFacade(RoomReserver calculator) {
			_calculator = calculator;
		}

		public ICollection<Guid> GetAvailableRoomTypes(DateTime from,DateTime to) {
			return _calculator.CalculateFreeRoomTypes(from,to);
		}
	}
}
