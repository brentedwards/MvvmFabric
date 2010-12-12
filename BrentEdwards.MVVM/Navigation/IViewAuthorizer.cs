using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrentEdwards.MVVM.Navigation
{
	public interface IViewAuthorizer
	{
		bool AuthorizeView(ViewTargets viewTarget);
	}
}
