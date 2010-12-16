using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrentEdwards.MVVM.Navigation;

namespace BrentEdwards.MVVM
{
	public abstract class ModalViewModelBase : ViewModelBase, IModalViewModel
	{
		public event EventHandler<RequestCloseEventArgs> RequestClose;

		protected void NotifyCloseRequest()
		{
			NotifyCloseRequest(true, null);
		}

		protected void NotifyCloseRequest(bool accepted)
		{
			NotifyCloseRequest(accepted, null);
		}

		protected void NotifyCloseRequest(bool accepted, object viewResult)
		{
			if (RequestClose != null)
			{
				RequestClose(this, new RequestCloseEventArgs(accepted, viewResult));
			}
		}
	}
}
