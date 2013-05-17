using GuestService.Backend.DAL;
using GuestService.Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestService.Backend {
	public class GuestServiceFacade {
		readonly GuestDataContext _context;

		public GuestServiceFacade(GuestDataContext context) {
			_context = context;
		} 

		public void Handle(Commands.StoreGuestInfo guestInfo) {
			if(_context.Guests.Find(guestInfo.ReservationId)!=null) {
				return;
			}
			_context.Guests.Add(new Guest { 
				Firstname = guestInfo.Firstname,
				Email = guestInfo.Email,
				Lastname = guestInfo.Lastname,
				Phonenumber = guestInfo.Phonenumber,
				ReservationId = guestInfo.ReservationId
			});

			_context.SaveChanges();
		}
	}
}
