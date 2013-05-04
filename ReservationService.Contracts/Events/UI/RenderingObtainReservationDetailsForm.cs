using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Contracts.Events.UI {
	public class RenderingObtainReservationDetailsForm {
		public System.Xml.Linq.XElement Form { get; set; }
		public Guid ReservationId{get;set;}
	}
}
