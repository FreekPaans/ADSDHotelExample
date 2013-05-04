using Castle.MicroKernel;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CustomerWebsite.WebInterface{
	class WindsorControllerFactory : DefaultControllerFactory{
		private  IWindsorContainer _container;
		public WindsorControllerFactory(IWindsorContainer container) {
			_container = container;
		}
		protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext,Type controllerType) {
			if(controllerType!=null) {
				try {
					return (IController)_container.Resolve(controllerType);
				}
				catch(ComponentNotFoundException) {
				}
			}
			return base.GetControllerInstance(requestContext,controllerType);
		}

		public override void ReleaseController(IController controller) {
			_container.Release(controller);
			base.ReleaseController(controller);
		}
	}
}
