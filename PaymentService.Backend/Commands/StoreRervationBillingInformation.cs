﻿using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Backend.Commands {
	public class StoreRervationBillingInformation : ICommand{
		public Guid ReservationId { get; set; }

		public string CreditCardNumber { get; set; }
		public string CreditCardExperiationDate{get;set;}

		public BillingAddress Address{get;set;}

		public class BillingAddress {
			public string Street{get;set;}
			public string PostalCode{get;set;}

			public string Lastname { get; set; }

			public string Firstname { get; set; }

			public string City { get; set; }
		}
	}
}
