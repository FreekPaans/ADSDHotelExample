using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerWebsite.Events {
	public class ShowingReservationSummary {
		public Guid ReservationId { get; set; }
		public string ReservationInformation{get;set;}
		public string GuestInformation{get;set;}
		public string PaymentInformation{get;set;}
		public string PricingInformation{get;set;}
	}
}
