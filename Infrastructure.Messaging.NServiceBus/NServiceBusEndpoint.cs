using Castle.Windsor;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure.Messaging.NServiceBus{
	public static class NServiceBusEndpoint {
		public static void StartBus(IWindsorContainer container) {
			global::NServiceBus.Configure.With().CastleWindsorBuilder(container).
				DefiningCommandsAs(c=>typeof(Infrastructure.Messaging.ICommand).IsAssignableFrom(c)).
				XmlSerializer().MsmqTransport().UnicastBus().
				
				SendOnly();
		}
	}
}