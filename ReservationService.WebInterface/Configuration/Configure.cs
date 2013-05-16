using Castle.MicroKernel.Registration;
using Infrastructure.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationService.WebInterface.Configuration {
	public class Configure:INeedToRegisterComponents {

		public void Register(Castle.Windsor.IWindsorContainer container) {
			container.Register(Component.For<CustomerWebsite.Contracts.ReservationSummary.IProvideReservationDetails>().ImplementedBy<ReservationDetailsProvider>().LifestyleTransient());
		}
	}
}