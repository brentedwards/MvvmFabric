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
		public ViewResult(FrameworkElement view)
		{
			View = view;
		}

		public FrameworkElement View { get; private set; }
	}
}
