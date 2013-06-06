namespace MessageProcessorHost {
	using Castle.Windsor;
	using Infrastructure.Lifecycle;
	using NServiceBus;
	using NServiceBus.Unicast;
	using System.Linq;
	using System.Transactions;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://nservicebus.com/GenericHost.aspx
	*/
	public class EndpointConfig:IConfigureThisEndpoint,AsA_Publisher,IWantCustomInitialization, INameThisEndpoint, IWantToRunWhenTheBusStarts {
		public void Init() {
			var container = new WindsorContainer();
			Configure.With().
				DefiningCommandsAs(m=>m.GetInterfaces().Contains(typeof(Infrastructure.Messaging.ICommand))).
				DefiningEventsAs(m=>m.GetInterfaces().Contains(typeof(Infrastructure.Messaging.IEvent))).
				CastleWindsorBuilder(container).PurgeOnStartup(true).
				IsolationLevel(IsolationLevel.ReadCommitted);

		
			ConfigureComponents.RunServiceConfiguration(container);
		}

		
		public string GetName() {
			return "adsd.MessageProcessor";
		}

		public IBus Bus{get;set;}

		public void Run() {
			var events = ConfigureComponents.GetAllTypes().Where(t=>t.GetInterfaces().Contains(typeof(Infrastructure.Messaging.IEvent)));

			foreach(var @event in events) {
				Bus.Subscribe(@event);
			}
		}
	}
}