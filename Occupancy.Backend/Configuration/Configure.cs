using Castle.MicroKernel.Registration;
using Infrastructure.Lifecycle;
using Occupancy.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occupancy.Backend.Configuration {
	public class Configure : INeedToRegisterComponents{
		public void Register(Castle.Windsor.IWindsorContainer container) {
			container.Register(Component.For<DataContext>().LifestyleTransient());
			UpdateDBSchema();
		}


		public static void UpdateDBSchema() {
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext,Migrations.Configuration>());
		}
	}
}
