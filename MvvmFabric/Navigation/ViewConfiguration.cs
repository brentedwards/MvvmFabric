using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MvvmFabric.Navigation
{
	/// <summary>
	/// ViewConfiguration sets up a binding relationship between a view
	/// and an optional view model.
	/// </summary>
	public sealed class ViewConfiguration
	{
		public ViewConfiguration(FrameworkElement view)
		{
			View = view;
		}

		public ViewConfiguration(FrameworkElement view, Object viewModel)
			: this(view)
		{
			ViewModel = viewModel;
		}

		public FrameworkElement View { get; private set; }
		public Object ViewModel { get; private set; }
	}
}
