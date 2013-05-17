using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.HTTP.Session {
	public interface ISessionStorage {
		object this[Guid idx] {get;set;}
		
	}
}
