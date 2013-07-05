using GuestService.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuestService.WebInterface.Controllers {
	public class FindReservationsByGuestNameController:Controller {
		readonly GuestDataContext _context;
		
		public FindReservationsByGuestNameController(GuestDataContext context) {
			_context = context;
		}
		public ActionResult Index(string name) {
			
			return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = _context.Guests.Where(g=>g.Lastname.StartsWith(name)).Select(g=>g.ReservationId).ToArray() };
		}
	}
}
