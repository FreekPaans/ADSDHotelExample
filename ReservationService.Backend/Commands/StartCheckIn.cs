﻿using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Backend.Commands {
	public class StartCheckIn : ICommand{
		public Guid ReservationId { get; set; }
	}
}
