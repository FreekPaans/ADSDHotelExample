using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Backend.DAL.Models {
	public class Reservation {
		[Key]
		public Guid ReservationId{get;set;}
		public Guid RoomTypeId{get;set;}
		public DateTime From{get;set;}
		public DateTime To{get;set;}
		public string Status { get; set; }
		
		public const string Pending = "Pending";
		public const string Committed = "Committed";


		
	}
}
