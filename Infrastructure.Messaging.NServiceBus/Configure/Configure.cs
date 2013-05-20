using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Infrastructure.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Messaging.NServiceBus.Configure {
	public class Configure : INeedToRegisterComponents{
		public void Register(IWindsorContainer container) {
			container.Register(Component.For<ICommandBus>().ImplementedBy<NServiceBusCommandBus>());
			container.Register(Component.For<IEventBus>().ImplementedBy<NServiceBusEventBus>());
		}
	}
}
