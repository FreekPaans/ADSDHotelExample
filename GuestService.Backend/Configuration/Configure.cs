using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CustomerWebsite.Contracts.ReservationSummary;
using GuestService.Backend.DAL;
using GuestService.Backend.DataProviders;
using Infrastructure.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestService.Backend.Configuration {
	public class Configure : INeedToRegisterComponents{
		public void Register(IWindsorContainer container) {
			container.Register(Component.For<IProvideGuestDetails>().ImplementedBy<GuestDetailsProvider>().LifestyleTransient());
			container.Register(Component.For<GuestDataContext>().LifestyleTransient());
			container.Register(Component.For<GuestServiceFacade>().LifestyleTransient());
		}
	}
}
