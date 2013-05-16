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
		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			Container = new WindsorContainer();

			ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(Container));

			//var handler = new ReservationService.WebInterface.Models.HttpRequestHandler();
			LoadBinAssemblies();

			var allLoadedTypes = Classes.
				From(AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()));

			Container.Register(allLoadedTypes.BasedOn<IController>().LifestyleTransient());
			
			var handlers = allLoadedTypes.
				BasedOn<IHandleHttpRequests>().LifestyleTransient();
			
			Container.Register(handlers.WithServiceBase());

			var pipeline = new WindsorHttpProcessingPipeline(Container);
			Container.Register(Component.For<IHttpProcessingPipeline>().Instance(pipeline));

			Infrastructure.Messaging.NServiceBus.Configure.Configure.Setup(Container);

			GuestService.Logic.Configure.Configure.Setup(Container);

			NServiceBusEndpoint.StartBus(Container);

			SetupDependencies(Container);

			CustomConfig(allLoadedTypes,Container);
		}

		private void CustomConfig(FromTypesDescriptor allLoadedTypes,WindsorContainer Container) {
			Container.Register(allLoadedTypes.BasedOn<INeedToRegisterComponents>().WithServiceBase());
			foreach(var toRegister in Container.ResolveAll<INeedToRegisterComponents>()) {
				toRegister.Register(Container);
			}
		}

		
		private void SetupDependencies(WindsorContainer Container) {
			Container.Register(Component.For<CustomerWebsite.WebInterface.ViewModels.ReservationSummaryViewModel.Provider>().LifestyleTransient());
		}

		private void LoadBinAssemblies() {
			var assemblies = new System.IO.DirectoryInfo(HttpRuntime.BinDirectory).GetFiles("*.dll", System.IO.SearchOption.AllDirectories);

			foreach(var assembly in assemblies) {
				Assembly.LoadFrom(assembly.FullName);
			}
		}

		public static WindsorContainer Container { get; set; }
	}
}