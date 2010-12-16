using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFabric.Navigation;

namespace MvvmFabric
{
	public abstract class ModalViewModelBase : ViewModelBase, IModalViewModel
	{
		public event EventHandler<RequestCloseEventArgs> RequestClose;

		protected internal void NotifyCloseRequest()
		{
			NotifyCloseRequest(true, null);
		}

		protected internal void NotifyCloseRequest(bool accepted)
		{
			NotifyCloseRequest(accepted, null);
		}

		protected internal void NotifyCloseRequest(bool accepted, object viewResult)
		{
			if (RequestClose != null)
			{
				RequestClose(this, new RequestCloseEventArgs(accepted, viewResult));
			}
		}
	}
}
