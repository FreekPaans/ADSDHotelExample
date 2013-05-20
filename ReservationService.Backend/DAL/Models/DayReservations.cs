using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Backend.DAL.Models {
	public class DayReservations {
		[Key]
		public DateTime Day{get;set;}
		[Key]
		public Guid RoomTypeId{get;set;}
		public int AvailableRooms{get;set;}
		//public string ReservationData{get;set;}
		//TODO:Test
		public byte[] Version{get;set;}
	}
}
