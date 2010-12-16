using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmFabric.Messaging
{
	public sealed class ActionReference
	{
		private WeakReference WeakReference { get; set; }

		public Delegate Target { get; private set; }

		public bool IsAlive
		{
			get { return WeakReference.IsAlive; }
		}

		public ActionReference(Delegate action)
		{
			Target = action;
			WeakReference = new WeakReference(action.Target);
		}
	}
}
