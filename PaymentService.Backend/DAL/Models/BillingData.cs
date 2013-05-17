using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Backend.DAL.Models {
	public class BillingData {
		[Key]
		public Guid ReservationId{get;set;}
		public string Firstname{get;set;}
		public string Lastname{get;set;}
		public string StreetName{get;set;}
		public string PostalCode{get;set;}
		public string City{get;set;}
		public string CreditCardNumber{get;set;}
		public DateTime CreditCardExpiration{get;set;}
	}
}
