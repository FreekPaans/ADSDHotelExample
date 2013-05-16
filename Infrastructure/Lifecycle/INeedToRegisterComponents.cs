using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Lifecycle {
	public interface INeedToRegisterComponents {
		void Register(IWindsorContainer container);
	}
}
