using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MvvmFabric.Navigation
{
	/// <summary>
	/// Dynamic view factory that will instantiate views and their associated view
	/// models based on given parameters.
	/// </summary>
	public sealed class ViewFactory : IViewFactory
	{
		private const String LOAD = "Load";

		private IViewConfigurationResolver ViewConfigResolver { get; set; }

		public ViewFactory(IViewConfigurationResolver viewConfigResolver)
		{
			ViewConfigResolver = viewConfigResolver;

			// A view model is required by default.
			IsViewModelRequiredForView = true;
		}

		/// <summary>
		/// Gets or sets whether a view model is required for a view.
		/// </summary>
		public bool IsViewModelRequiredForView { get; set; }

		/// <summary>
		/// Builds a view for a specific <see cref="ViewTargets"/> value.
		/// </summary>
		/// <param name="viewTarget">
		/// The <see cref="ViewTargets"/> value to build the view for.
		/// </param>
		/// <returns>Returns a <see cref="ViewResult"/> containing the view.</returns>
		public ViewResult Build(ViewTargets viewTarget)
		{
			return Build(viewTarget, null);
		}

		/// <summary>
		/// Builds a view for a specific <see cref="ViewTargets"/> value with an optional
		/// parameter used to load the view.
		/// </summary>
		/// <param name="viewTarget">
		/// The <see cref="ViewTargets"/> value to build the view for.
		/// </param>
		/// <param name="viewParams">The parameter used to load the view.</param>
		/// <returns>Returns a <see cref="ViewResult"/> containing the view.</returns>
		public ViewResult Build(ViewTargets viewTarget, Object viewParams)
		{
			var viewConfig = ViewConfigResolver.ResolveConfiguration(viewTarget);
			var view = viewConfig.View;

			if (viewConfig.ViewModel != null)
			{
				// There was a view model explicitly defined, use it.
				view.DataContext = viewConfig.ViewModel;
			}

			LoadViewModelHelper(view.DataContext, viewParams);

			return new ViewResult(view, viewTarget);
		}

		private void LoadViewModelHelper(Object viewModel, Object viewParams)
		{
			if (IsViewModelRequiredForView && viewModel == null)
			{
				throw new ArgumentNullException("viewModel", "A view is required to have a view model.");
			}
			else if (!IsViewModelRequiredForView && viewModel == null)
			{
				return;
			}

			var viewModelType = viewModel.GetType();

			Object[] parms = null;
			Type[] paramTypes = null;

			if (viewParams != null)
			{
				// Attempt to find a Load method on the view model which takes the given parameters.
				paramTypes = new Type[] { viewParams.GetType() };
				parms = new Object[] { viewParams };
			}
			else
			{
				paramTypes = new Type[] { };
			}

			var methodInfo = viewModelType.GetMethod(LOAD, paramTypes);

			// If view params are given, a Load method which takes those params is required.  If
			// no params are given, a Load method which takes no params is optional.
			if (methodInfo == null && viewParams != null)
			{
				throw new ArgumentException(
					String.Format("'{0}' does not have a 'Load' method with the matching parameters.",
					viewModelType.Name));
			}
			else if (methodInfo != null)
			{
				methodInfo.Invoke(viewModel, BindingFlags.ExactBinding, null, parms, null);
			}
		}
	}
}
