using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrentEdwards.MVVM.Navigation
{
	public interface IViewFactory
	{
		ViewResult Build(ViewTargets viewTarget);
		ViewResult Build(ViewTargets viewTarget, Object viewParams);
	}
}
