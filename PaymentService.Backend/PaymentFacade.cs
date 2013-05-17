using PaymentService.Backend.DAL;
using PaymentService.Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Backend {
	public class PaymentFacade {
		readonly PaymentDataContext _context;
		public PaymentFacade(PaymentDataContext context) {
			_context = context;
		}
		public void Handle(Commands.StoreRervationBillingInformation command) {
			if(_context.BillingData.Find(command.ReservationId)!=null) {
				//be idempotent, should probably also be in transaction
				return;
			}

			_context.BillingData.Add(new BillingData { 
				City = command.Address.City,
				CreditCardExpiration = GetMonth(command.CreditCardExperiationDate),
				CreditCardNumber = command.CreditCardNumber,
				Firstname = command.Address.Firstname,
				Lastname = command.Address.Lastname,
				PostalCode = command.Address.PostalCode,
				ReservationId = command.ReservationId,
				StreetName = command.Address.Street
			});
			_context.SaveChanges();
		}

		private static DateTime GetMonth(string mmYYYY) {
			var split = mmYYYY.Split('-');
			return new DateTime(int.Parse(split[1]),int.Parse(split[0])+1,1);
		}
	}
}
