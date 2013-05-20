using Castle.MicroKernel.Registration;
using Infrastructure.Lifecycle;
using ITOps.PaymentProvider.Contracts.AcquireHoldForReservationCancellationFeeContracts;
using PricingService.Backend.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingService.Backend.Configuration {
	public class Configure : INeedToRegisterComponents{
		public void Register(Castle.Windsor.IWindsorContainer container) {
			container.Register(Component.For<IProvidePricingData>().ImplementedBy<AcquireHoldPricingDataProvider>().LifestyleTransient());
		}
	}
}
