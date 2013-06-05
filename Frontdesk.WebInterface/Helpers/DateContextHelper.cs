using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontdesk.WebInterface.Helpers {
	public static class DateContextHelper {
		const string DateContextCookieName = "DateContext";
		public static DateTime Now() {
			var cookie = HttpContext.Current.Request.Cookies[DateContextCookieName];
			if(cookie!= null) {
				return DateTime.Parse(cookie.Value).Date;
			}
			return DateTime.Now.Date;
		}

		public static string CookieName {
			get {
				return DateContextCookieName;
			}
		}
	}
}