using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmFabric.Navigation
{
	public interface IViewAuthorizer
	{
		bool AuthorizeView(ViewTargets viewTarget);
	}
}
