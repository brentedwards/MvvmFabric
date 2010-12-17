using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmFabric.Navigation
{
	/// <summary>
	/// Interface for the view model of a modal view.
	/// </summary>
	public interface IModalViewModel
	{
		/// <summary>
		/// An event which indicates when a view is requested to be closed.
		/// </summary>
		event EventHandler<RequestCloseEventArgs> RequestClose;
	}
}
