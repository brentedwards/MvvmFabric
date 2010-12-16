using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmFabric.Messaging
{
	public sealed class ModalViewClosedMessage
	{
		public ViewTargets ViewTarget { get; private set; }
		public bool Accepted { get; private set; }
		public object ViewResult { get; private set; }

		public ModalViewClosedMessage(ViewTargets viewTarget, bool accepted)
		{
			ViewTarget = viewTarget;
			Accepted = accepted;
		}

		public ModalViewClosedMessage(ViewTargets viewTarget, bool accepted, object viewResult)
			: this(viewTarget, accepted)
		{
			ViewResult = viewResult;
		}
	}
}
