using Infrastructure.Dates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Backend.DAL.Models {
	public class ReservationStatus {
		[Key]
		public Guid ReservationId{get;set;}
		public Guid RoomTypeId{get;set;}
		public DateTime From{get;set;}
		public DateTime To{get;set;}
		public string FlowStatus { get; set; }

		public const string FlowStarted = "FlowStarted";
		public const string Committed = "Committed";

		public DateTime ReservedAt { get; set; }

		public string CancellationFeeStatus {get;set;}

		
		public bool IsCheckingIn { get; set; }
		public bool IsCheckedIn { get; set; }
		public bool CheckInFailed { get; set; }
		public string FailedReason { get; set; }
	}

	public class ReservationCancellationFeeStatus {
		public const string Pending = "Pending";
		public const string Acquired = "Acquired";
		public const string Denied = "Denied";
		
		public const string NotApplicable = "NotApplicable";

		//todo:implement this
		public const string TimedOut = "TimedOut";
	} 
}
