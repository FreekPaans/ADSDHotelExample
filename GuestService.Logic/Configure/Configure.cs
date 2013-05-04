using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestService.Logic.Configure {
	public static class Configure {
		public static void Setup(IWindsorContainer container) {
			container.Register(Component.For<GuestDatabase>().LifestyleTransient());
		}
	}
}
