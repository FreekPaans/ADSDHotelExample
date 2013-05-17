using Castle.MicroKernel.Registration;
using Infrastructure.Lifecycle;
using ReservationService.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Backend.Configuration {
	public class Configure : INeedToRegisterComponents{
		public void Register(Castle.Windsor.IWindsorContainer container) {
			container.Register(Component.For<ReservationFacade>().LifestyleTransient());
			container.Register(Component.For<ReservationDataContext>().LifestyleTransient());
		}
	}
}
