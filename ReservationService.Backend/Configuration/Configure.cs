using Castle.MicroKernel.Registration;
using Infrastructure.Lifecycle;
using ReservationService.Backend.DAL;
using ReservationService.Backend.Logic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Backend.Configuration {
	public class Configure : INeedToRegisterComponents{
		public void Register(Castle.Windsor.IWindsorContainer container) {
			container.Register(Component.For<ReservationFacade>().LifestyleTransient());
			container.Register(Component.For<ReservationDataContext>().LifestyleTransient());
			container.Register(Component.For<RoomReserver>().LifestyleTransient().DependsOn(
				Dependency.OnValue("roomConfiguration",  new Dictionary<Guid,int> {
					{ Guid.Parse("3CC3A362-D802-46E6-A27D-22711618AE2D"), 100 },
					{ Guid.Parse("267658E6-3498-4EEC-B269-616FBD07FE9B"), 50 },
					{ Guid.Parse("D9156B38-843E-43C3-BC82-7E4558BD1328"), 1 },
				})
			));
			

			UpdateDBSchema();
			//Database.SetInitializer(new MigrateDatabaseToLatestVersion<ReservationDataContext, Migrations.Configuration>());
		}
		
		public static void UpdateDBSchema() {
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<ReservationDataContext, Migrations.Configuration>());
		}
	}
}

