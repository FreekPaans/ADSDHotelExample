﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontdesk.WebInterface.ViewModels {
	public class ReservationSearchSummaryViewModel {
		public Guid ReservationId{get;set;}
		public Contracts.ReservationCheckin.ReservationDetails ReservationDetails { get; set; }

		public Contracts.ReservationCheckin.GuestDetails GuestDetails { get; set; }
	}
}