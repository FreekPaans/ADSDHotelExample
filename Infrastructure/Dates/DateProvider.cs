using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Dates {
	public static class DateProvider {
		public static DateTime Now {
			get {
				if(HttpContext.Current!=null) {
					var dateCookie = HttpContext.Current.Request.Cookies[DateContextCookieName];
					if(dateCookie!= null) {
						var date = DateTime.Parse(dateCookie.Value).Date;

						var timeCookie = HttpContext.Current.Request.Cookies[TimeContextCookieName];
						if(timeCookie!=null) {
							date = date.Add(TimeSpan.Parse(timeCookie.Value));
						}
						else {
							date = date.Add(DateTime.Now.TimeOfDay);
						}
						
						return date;
					}
				}
				if(_overrideDateTime!=null) {
					return _overrideDateTime.Value;
				}

				return DateTime.Now;
			}
		}

		public const string DateContextCookieName = "DateContext";

		public static string TimeContextCookieName = "TimeContext";

		public static bool HasOverriddenTime {
			get {
				return HttpContext.Current!=null && HttpContext.Current.Request.Cookies[TimeContextCookieName]!=null;
			}
		}

		[ThreadStatic]
		static DateTime? _overrideDateTime;

		public static IDisposable OverrideDateTime(DateTime? dt) {
			return new Override(dt);
			
		}

		class Override : IDisposable {
			public Override(DateTime? dt) {
				_overrideDateTime = dt;
			}
			public void Dispose() {
				_overrideDateTime = null;
			}
		}
	}
}
