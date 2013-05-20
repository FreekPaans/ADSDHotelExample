namespace MessageProcessorHost {
	using Castle.Windsor;
	using Infrastructure.Lifecycle;
	using NServiceBus;
	using System.Linq;
	using System.Transactions;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://nservicebus.com/GenericHost.aspx
	*/
	public class EndpointConfig:IConfigureThisEndpoint,AsA_Server,IWantCustomInitialization, INameThisEndpoint {
		public void Init() {
			var container = new WindsorContainer();
			Configure.With().CastleWindsorBuilder(container).
				DefiningCommandsAs(m=>m.GetInterfaces().Contains(typeof(Infrastructure.Messaging.ICommand))).IsolationLevel(IsolationLevel.ReadCommitted);
			ConfigureComponents.RunServiceConfiguration(container);
		}

		public string GetName() {
			return "adsd.MessageProcessor";
		}
	}
}