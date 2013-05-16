using Castle.MicroKernel.Registration;
using CustomerWebsite.Contracts.ReservationSummary;
using Infrastructure.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentService.WebInterface.Configuration {
	public class Configure : INeedToRegisterComponents{
		public void Register(Castle.Windsor.IWindsorContainer container) {
			container.Register(Component.For<IProvidePaymentDetails>().ImplementedBy<PaymentDetailsProvider>().LifestyleTransient());
		}
	}
}