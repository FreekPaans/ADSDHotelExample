using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Infrastructure.HTTP.MVC;
using Infrastructure.HTTP.ProcessingPipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Infrastructure.Lifecycle {
	public static class ConfigureComponents {
		
		public static void RunServiceConfiguration(IWindsorContainer container) {
			container.Register(Classes.From(GetAllTypes()).BasedOn<INeedToRegisterComponents>().WithServiceBase());

			foreach(var toRegister in container.ResolveAll<INeedToRegisterComponents>()) {
				toRegister.Register(container);
			}
		}

		public static ICollection<Type> GetAllTypes() {
			 return AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).ToArray();
		}

		static void LoadAssembliesInBinFolder() {
			var assemblies = new System.IO.DirectoryInfo(HttpRuntime.BinDirectory).GetFiles("*.dll", System.IO.SearchOption.AllDirectories);

			foreach(var assembly in assemblies) {
				Assembly.LoadFrom(assembly.FullName);
			}
		}

		public static void SetupMVC(IWindsorContainer container) {
			LoadAssembliesInBinFolder();

			ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

			ConfigureComponents.LoadAssembliesInBinFolder();

			var allLoadedTypes= Classes.From(ConfigureComponents.GetAllTypes());

			RegisterControllers(container, allLoadedTypes);
			RegisterHttpHandlers(container,allLoadedTypes);
			SetupPipeline(container);

			RunServiceConfiguration(container);
		}

		private static void SetupPipeline(IWindsorContainer container) {
			var pipeline = new WindsorHttpProcessingPipeline(container);
			container.Register(Component.For<IHttpProcessingPipeline>().Instance(pipeline));
		}

		private static void RegisterHttpHandlers(IWindsorContainer container, FromTypesDescriptor allLoadedTypes) {
			container.Register(allLoadedTypes.BasedOn<IHandleHttpRequests>().LifestyleTransient().WithServiceBase());
		}

		private static void RegisterControllers(IWindsorContainer container, FromTypesDescriptor allLoadedTypes) {
			container.Register(allLoadedTypes.BasedOn<IController>().LifestyleTransient());
		}
	}
}
