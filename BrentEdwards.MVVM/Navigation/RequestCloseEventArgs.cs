using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrentEdwards.MVVM.Navigation
{
	public sealed class RequestCloseEventArgs : EventArgs
	{
		public bool Accepted { get; private set; }
		public object ViewResult { get; private set; }

		public RequestCloseEventArgs(bool accepted)
		{
			Accepted = accepted;
		}

		public RequestCloseEventArgs(bool accepted, object viewResult)
			: this(accepted)
		{
			ViewResult = viewResult;
		}
	}
}
