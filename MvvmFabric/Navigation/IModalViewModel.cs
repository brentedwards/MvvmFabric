using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmFabric.Navigation
{
	public interface IModalViewModel
	{
		event EventHandler<RequestCloseEventArgs> RequestClose;
	}
}
