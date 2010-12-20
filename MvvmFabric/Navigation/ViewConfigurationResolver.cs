using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MvvmFabric.Navigation
{
	/// <summary>
	/// An implementation for IViewConfigurationResolver.  A View Configuration Resolver resolves
	/// <see cref="ViewConfiguration"/>s for specific <see cref="ViewTargets"/>.
	/// </summary>
	public sealed class ViewConfigurationResolver : IViewConfigurationResolver
	{
		private class ViewPair
		{
			public Type ViewType { get; set; }
			public Type ViewModelType { get; set; }
		}

		private Dictionary<ViewTargets, ViewPair> ViewConfigurations { get; set; }

		/// <summary>
		/// Constructor for ViewConfigurationResolver.
		/// </summary>
		public ViewConfigurationResolver()
		{
			ViewConfigurations = new Dictionary<ViewTargets, ViewPair>();
		}

		/// <summary>
		/// Registers the type <typeparamref name="TView"/> to a spefic
		/// <see cref="ViewTargets"/> value.
		/// </summary>
		/// <typeparam name="TView">The type of the view to be registered.</typeparam>
		/// <param name="viewTarget">The <see cref="ViewTargets"/> value to register.</param>
		public void RegisterViewConfiguration<TView>(ViewTargets viewTarget)
			where TView : FrameworkElement
		{
			ViewConfigurations[viewTarget] = new ViewPair() { ViewType = typeof(TView) };
		}

		/// <summary>
		/// Registers the type <typeparamref name="TView"/> to a spefic
		/// <see cref="ViewTargets"/> value with an explicit view model type.
		/// </summary>
		/// <typeparam name="TView">The type of the view to be registered.</typeparam>
		/// <typeparam name="TViewModel">
		/// The type of the view model to be bound to the view.
		/// </typeparam>
		/// <param name="viewTarget">The <see cref="ViewTargets"/> value to register.</param>
		public void RegisterViewConfiguration<TView, TViewModel>(ViewTargets viewTarget)
			where TView : FrameworkElement
		{
			ViewConfigurations[viewTarget] = new ViewPair() { ViewType = typeof(TView), ViewModelType = typeof(TViewModel) };
		}

		/// <summary>
		/// Resolves the <see cref="ViewConfiguration"/> associated with the
		/// <see cref="ViewTargets"/> value.
		/// </summary>
		/// <param name="viewTarget">The <see cref="ViewTargets"/> value to resolve.</param>
		/// <returns>
		/// Returns the <see cref="ViewConfiguration"/> associated with the
		/// <see cref="ViewTargets"/> value.
		/// </returns>
		public ViewConfiguration ResolveConfiguration(ViewTargets viewTarget)
		{
			var viewPair = ViewConfigurations[viewTarget];

			var view = Activator.CreateInstance(viewPair.ViewType) as FrameworkElement;

			object viewModel = null;
			if (viewPair.ViewModelType != null)
			{
				viewModel = Activator.CreateInstance(viewPair.ViewModelType);
			}

			return new ViewConfiguration(view, viewModel);
		}
	}
}
