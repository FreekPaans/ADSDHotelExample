﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerWebsite.Contracts.Events {
	public class SearchingForRooms {
		public DateTime From { get; set; }
		public DateTime Till { get; set; }
	}
}
