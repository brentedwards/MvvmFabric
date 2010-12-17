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
		/// <summary>
		/// Gets the view.
		/// </summary>
		public FrameworkElement View { get; private set; }

		/// <summary>
		/// Gets the optional view model.
		/// </summary>
		public Object ViewModel { get; private set; }

		/// <summary>
		/// Constructor for ViewConfiguration.
		/// </summary>
		/// <param name="view">The view.</param>
		public ViewConfiguration(FrameworkElement view)
		{
			View = view;
		}

		/// <summary>
		/// Constructor for ViewConfiguration which takes an optional view model.
		/// </summary>
		/// <param name="view">The view.</param>
		/// <param name="viewModel">The view model.</param>
		public ViewConfiguration(FrameworkElement view, Object viewModel)
			: this(view)
		{
			ViewModel = viewModel;
		}
	}
}
