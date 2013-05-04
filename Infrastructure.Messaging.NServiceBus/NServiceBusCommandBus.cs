using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Messaging.NServiceBus {
	class NServiceBusCommandBus : ICommandBus{
		readonly IBus _bus;
		public NServiceBusCommandBus(IBus bus) {
			_bus = bus;
		}
		public void Send(ICommand command) {
			_bus.Send(command);
		}
	}
}
