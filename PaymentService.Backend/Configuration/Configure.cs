using Castle.MicroKernel.Registration;
using CustomerWebsite.Contracts.ReservationSummary;
using Infrastructure.Lifecycle;
using PaymentService.Backend.DAL;
using PaymentService.Backend.DataProviders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PaymentService.Backend.Configuration {
	public class Configure : INeedToRegisterComponents{
		public void Register(Castle.Windsor.IWindsorContainer container) {
			container.Register(Component.For<IProvidePaymentDetails>().ImplementedBy<PaymentDetailsProvider>().LifestyleTransient());
			container.Register(Component.For<PaymentFacade>().LifestyleTransient());
			container.Register(Component.For<PaymentDataContext>().LifestyleTransient());

			Database.SetInitializer(new MigrateDatabaseToLatestVersion<PaymentDataContext, Migrations.Configuration>());
		}
	}
}