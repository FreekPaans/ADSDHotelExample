using Castle.MicroKernel.Registration;
using Infrastructure.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Infrastructure.HTTP.Session.AspNet {
	public class AspNetSessionStore : ISessionStorage{
		public object this[Guid idx] {
			get {
				return HttpContext.Current.Session[idx.ToString()];
			}
			set {
				HttpContext.Current.Session[idx.ToString()] = value;
			}
		}

		
	}

	public class Configure:INeedToRegisterComponents {
		public void Register(Castle.Windsor.IWindsorContainer container) {
			container.Register(Component.For<ISessionStorage>().ImplementedBy<AspNetSessionStore>());
		}
	}

}
