﻿using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Infrastructure.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestService.Backend.Configure {
	public class Configure : INeedToRegisterComponents{
		public void Register(IWindsorContainer container) {
			container.Register(Component.For<GuestDatabase>().LifestyleTransient());
		}
	}
}