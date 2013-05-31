using Castle.MicroKernel.Registration;
using Infrastructure.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Emails.NetSmtpClient {
	public class Configure : INeedToRegisterComponents{
		public void Register(Castle.Windsor.IWindsorContainer container) {
			container.Register(Component.For<ISmtpClient>().ImplementedBy<SmtpClientWrapper>() .LifestyleTransient());
		}
	}
}
