using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Lifecycle {
	public static class ConfigureComponents {
		
		public static void RunServiceConfiguration(IWindsorContainer container) {
			container.Register(GetAllTypes().BasedOn<INeedToRegisterComponents>().WithServiceBase());

			foreach(var toRegister in container.ResolveAll<INeedToRegisterComponents>()) {
				toRegister.Register(container);
			}
		}

		public static Castle.MicroKernel.Registration.FromTypesDescriptor GetAllTypes() {
			 return Classes.
				From(AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()));;
		}
	}
}
