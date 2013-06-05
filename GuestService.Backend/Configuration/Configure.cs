using Castle.MicroKernel.Registration;
using Castle.Windsor;
using GuestService.Backend.DAL;
using Infrastructure.Lifecycle;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestService.Backend.Configuration {
	public class Configure : INeedToRegisterComponents{
		public void Register(IWindsorContainer container) {
			
			container.Register(Component.For<GuestDataContext>().LifestyleTransient());
			container.Register(Component.For<GuestServiceFacade>().LifestyleTransient());

			Database.SetInitializer(new MigrateDatabaseToLatestVersion<GuestDataContext, Migrations.Configuration>());
		}
	}
}
