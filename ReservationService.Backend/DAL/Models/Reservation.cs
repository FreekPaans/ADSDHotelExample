using Infrastructure.Dates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

		public DateTime ReservedAt { get; set; }

		public string CancellationFeeStatus {get;set;}

		[NotMapped]
		public bool CanCheckIn {
			get {
				return IsInCheckinPeriod() && Status == Committed && new [] { ReservationCancellationFeeStatus.Acquired, ReservationCancellationFeeStatus.Pending }.Contains( CancellationFeeStatus);
			}
		}

		readonly static TimeSpan CheckinTime = TimeSpan.FromHours(12);

		public string GetFriendlyStatus() {
			if(!IsInCheckinPeriod()) {
				if(DateProvider.Now<From) {
					return "Reservation does not start today";
				}
				if(DateProvider.Now<From.Add(CheckinTime)) {
					return string.Format("Check in starts at {0}", CheckinTime);
				}
				return "Reservation cancelled because checkin date was missed";
			}

			if(Status==Committed && CancellationFeeStatus ==ReservationCancellationFeeStatus.Acquired) {
				return "Reservation ready for checkin";
			}

			if(CancellationFeeStatus == ReservationCancellationFeeStatus.Pending) {
				return "Cancellation fee not yet acquired, reservation still ready for checkin";
			}

			if(Status==Pending) {
				return "Reservation was never finalized";
			}
			

			if(CancellationFeeStatus == ReservationCancellationFeeStatus.Denied) {
				return "Cancellation fee was denied";
			}

			//TODO: should throw exception here or at least log it
			return "Unknown status";
		}

		private bool IsInCheckinPeriod() {
			return DateProvider.Now >= From.Add(CheckinTime) && DateProvider.Now < From.AddDays(1);
		}
	}

	public class ReservationCancellationFeeStatus {
		public const string Pending = "Pending";
		public const string Acquired = "Acquired";
		public const string Denied = "Denied";

	} 
}
