using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Infrastructure.HTTP.ProcessingPipeline;
using System.Reflection;
using Infrastructure.Lifecycle;


namespace CustomerWebsite.WebInterface {
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication:System.Web.HttpApplication {
		static private FromTypesDescriptor AllLoadedTypes;
		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			Container = new WindsorContainer();

			ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(Container));

			LoadAssembliesInBinFolder();

			AllLoadedTypes= Classes.From(ConfigureComponents.GetAllTypes());
				

			RegisterControllers();
			RegisterHttpHandlers();
			SetupPipeline();

			NServiceBusEndpoint.StartBus(Container);
			
			ConfigureComponents.RunServiceConfiguration(Container);
		}

		private static void SetupPipeline() {
			var pipeline = new WindsorHttpProcessingPipeline(Container);
			Container.Register(Component.For<IHttpProcessingPipeline>().Instance(pipeline));
		}

		private void RegisterHttpHandlers() {
			Container.Register(AllLoadedTypes.BasedOn<IHandleHttpRequests>().LifestyleTransient().WithServiceBase());
		}

		private void RegisterControllers() {
			Container.Register(AllLoadedTypes.BasedOn<IController>().LifestyleTransient());
		}


		private void LoadAssembliesInBinFolder() {
			var assemblies = new System.IO.DirectoryInfo(HttpRuntime.BinDirectory).GetFiles("*.dll", System.IO.SearchOption.AllDirectories);

			foreach(var assembly in assemblies) {
				Assembly.LoadFrom(assembly.FullName);
			}
		}

		public static WindsorContainer Container { get; set; }
	}
}