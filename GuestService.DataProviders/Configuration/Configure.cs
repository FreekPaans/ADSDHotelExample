using Castle.MicroKernel.Registration;
using Infrastructure.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuestService.DataProviders.Configuration {
	public class Configure : INeedToRegisterComponents{
		public void Register(Castle.Windsor.IWindsorContainer container) {
			container.Register(Component.For<
				CustomerWebsite.Contracts.ReservationSummary.IProvideGuestDetails,
				ITOps.ReservationCustomerEmails.Contracts.IProvideRecipientDetails,
				Frontdesk.Contracts.ReservationCheckin.IProvideGuestDetails
				>().ImplementedBy<GuestDetailsProvider>().LifestyleTransient());
		}
	}
}
