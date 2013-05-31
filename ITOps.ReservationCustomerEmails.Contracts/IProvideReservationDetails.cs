using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITOps.ReservationCustomerEmails.Contracts {
	public interface IProvideReservationDetails {
		ReservationDetails GetDetails(Guid reservationId);
	}

	public class ReservationDetails {
		public  DateTime ArrivalDate { get; set; }
		public  DateTime CheckoutDate { get; set; }
	}
}
