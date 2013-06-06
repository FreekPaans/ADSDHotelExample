using ITOps.PaymentProvider.Contracts.Events;
using PaymentService.Backend.DAL;
using PaymentService.Backend.DAL.Models;
using PaymentService.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Backend {
	public class PaymentFacade {
		readonly PaymentDataContext _context;
		readonly Infrastructure.Messaging.ICommandBus _commandBus;
		readonly Infrastructure.Messaging.IEventBus _eventBus;
		public PaymentFacade(PaymentDataContext context, Infrastructure.Messaging.ICommandBus commandBus, Infrastructure.Messaging.IEventBus eventBus) {
			_context = context;
			_commandBus = commandBus;
			_eventBus = eventBus;
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


		public void Handle(ReservationService.Contracts.Events.Business.RoomsForReservationAcquired @event) {
			_commandBus.Send(new ITOps.PaymentProvider.Commands.AcquireHoldForReservationCancellationFee { ReservationId = @event.ReservationId });
			_eventBus.Publish(new AcquiringHoldForReservationCancellationFee { ReservationId = @event.ReservationId });
		}

		public void Handle(ITOps.PaymentProvider.Contracts.Events.CancellationFeeHoldAcquiredFromCreditCard @event) {
			_eventBus.Publish(new CancellationFeeHoldAcquired { ReservationId = @event.ReservationId });
		}

		public void Handle(ITOps.PaymentProvider.Contracts.Events.CancellationFeeHoldDeniedFromCreditCard @event) {
			_eventBus.Publish(new CancellationFeeHoldDenied { ReservationId = @event.ReservationId });
		}
	}
}
