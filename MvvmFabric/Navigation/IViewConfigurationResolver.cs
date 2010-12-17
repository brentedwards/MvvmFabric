using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MvvmFabric.Navigation
{
	/// <summary>
	/// Interface for a View Configuration Resolver.  A View Configuration Resolver resolves
	/// <see cref="ViewConfiguration"/>s for specific <see cref="ViewTargets"/>.
	/// </summary>
	public interface IViewConfigurationResolver
	{
		/// <summary>
		/// Registers the type <typeparamref name="TView"/> to a spefic
		/// <see cref="ViewTargets"/> value.
		/// </summary>
		/// <typeparam name="TView">The type of the view to be registered.</typeparam>
		/// <param name="viewTarget">The <see cref="ViewTargets"/> value to register.</param>
		void RegisterViewConfiguration<TView>(ViewTargets viewTarget)
			where TView : FrameworkElement;

		/// <summary>
		/// Registers the type <typeparamref name="TView"/> to a spefic
		/// <see cref="ViewTargets"/> value with an explicit view model type.
		/// </summary>
		/// <typeparam name="TView">The type of the view to be registered.</typeparam>
		/// <typeparam name="TViewModel">
		/// The type of the view model to be bound to the view.
		/// </typeparam>
		/// <param name="viewTarget">The <see cref="ViewTargets"/> value to register.</param>
		void RegisterViewConfiguration<TView, TViewModel>(ViewTargets viewTarget)
			where TView : FrameworkElement;

		/// <summary>
		/// Resolves the <see cref="ViewConfiguration"/> associated with the
		/// <see cref="ViewTargets"/> value.
		/// </summary>
		/// <param name="viewTarget">The <see cref="ViewTargets"/> value to resolve.</param>
		/// <returns>
		/// Returns the <see cref="ViewConfiguration"/> associated with the
		/// <see cref="ViewTargets"/> value.
		/// </returns>
		ViewConfiguration ResolveConfiguration(ViewTargets viewTarget);
	}
}
