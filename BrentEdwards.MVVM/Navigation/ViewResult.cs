using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BrentEdwards.MVVM.Navigation
{
	/// <summary>
	/// ViewResult is the result of the dynamic build process.
	/// </summary>
	public class ViewResult
	{
		public FrameworkElement View { get; private set; }
		public ViewTargets ViewTarget { get; private set; }
		
		public ViewResult(FrameworkElement view, ViewTargets viewTarget)
		{
			View = view;
			ViewTarget = viewTarget;
		}
	}
}
