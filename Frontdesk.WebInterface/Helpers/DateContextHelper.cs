using Infrastructure.Dates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontdesk.WebInterface.Helpers {
	public static class DateContextHelper {
		
		public static DateTime Now() {
			return DateProvider.Now;
		}

		public static string DateCookieName {
			get {
				return DateProvider.DateContextCookieName;
			}
		}

		public static string TimeCookieName {
			get {
				return DateProvider.TimeContextCookieName;
			}
		}

		public static string TimeOverride {
			get {
				if(DateProvider.HasOverriddenTime) {
					return DateProvider.Now.TimeOfDay.ToString();
				}
				return "";
			}
		}
	}
}