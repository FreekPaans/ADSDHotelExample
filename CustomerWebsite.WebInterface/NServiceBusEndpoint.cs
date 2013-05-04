using Castle.Windsor;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerWebsite.WebInterface {
	public static class NServiceBusEndpoint {
		public static void StartBus(IWindsorContainer container) {
			Configure.With().CastleWindsorBuilder(container).
				DefiningCommandsAs(c=>typeof(Infrastructure.Messaging.ICommand).IsAssignableFrom(c)).
				XmlSerializer().MsmqTransport().UnicastBus().
				
				SendOnly();
		}
	}
}