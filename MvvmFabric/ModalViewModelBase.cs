using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFabric.Navigation;

namespace MvvmFabric
{
	/// <summary>
	/// Base class for Modal View Models.
	/// </summary>
	public abstract class ModalViewModelBase : ViewModelBase, IModalViewModel
	{
		/// <summary>
		/// An event which indicates when a view is requested to be closed.
		/// </summary>
		public event EventHandler<RequestCloseEventArgs> RequestClose;

		/// <summary>
		/// Notifies any subscribers that the view is requested to close.
		/// </summary>
		protected internal void NotifyCloseRequest()
		{
			NotifyCloseRequest(true, null);
		}

		/// <summary>
		/// Notifies any subscribers that the view is requested to close.
		/// </summary>
		/// <param name="accepted">Indicates whether the view is accepted or cancelled.</param>
		protected internal void NotifyCloseRequest(bool accepted)
		{
			NotifyCloseRequest(accepted, null);
		}

		/// <summary>
		/// Notifies any subscribers that the view is requested to close.
		/// </summary>
		/// <param name="accepted">Indicates whether the view is accepted or cancelled.</param>
		/// <param name="viewResult">Optional result returned by the view.</param>
		protected internal void NotifyCloseRequest(bool accepted, object viewResult)
		{
			if (RequestClose != null)
			{
				RequestClose(this, new RequestCloseEventArgs(accepted, viewResult));
			}
		}
	}
}
