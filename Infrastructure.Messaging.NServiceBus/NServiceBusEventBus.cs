using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Messaging.NServiceBus {
	class NServiceBusEventBus : IEventBus{
		readonly IBus _bus;
		public NServiceBusEventBus(IBus bus) {
			_bus = bus;
		}
		public void Publish(IEvent @event) {
			_bus.Publish(@event);
		}
	}
}
