using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOps.PaymentProvider.Commands {
	public class AcquireHoldForFullAmount : ICommand{
		public Guid ReservationId { get; set; }
	}
}
