using Castle.MicroKernel.Registration;
using Infrastructure.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReservationService.DataProviders.Configuration {
	public class Configure : INeedToRegisterComponents{
		public void Register(Castle.Windsor.IWindsorContainer container) {
			container.Register(Component.For<ITOps.PaymentProvider.Contracts.AcquireHoldForReservationCancellationFeeContracts.IProvideReservationData>().ImplementedBy<AcquireHoldDataProvider>().LifestyleTransient());
			container.Register(Component.For<ITOps.ReservationCustomerEmails.Contracts.IProvideReservationDetails,Frontdesk.Contracts.ReservationCheckin.IProvideReservationDetails>().ImplementedBy<CustomerEmailDetailsProvider>().LifestyleTransient());
		}
	}
}
