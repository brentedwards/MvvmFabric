using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmFabric.Navigation
{
	/// <summary>
	/// Interface for a View Factory.  A View Factory builds views for specific
	/// <see cref="ViewTargets"/> values.
	/// </summary>
	public interface IViewFactory
	{
		/// <summary>
		/// Gets or sets whether a view model is required for a view.
		/// </summary>
		bool IsViewModelRequiredForView { get; set; }

		/// <summary>
		/// Builds a view for a specific <see cref="ViewTargets"/> value.
		/// </summary>
		/// <param name="viewTarget">
		/// The <see cref="ViewTargets"/> value to build the view for.
		/// </param>
		/// <returns>Returns a <see cref="ViewResult"/> containing the view.</returns>
		ViewResult Build(ViewTargets viewTarget);

		/// <summary>
		/// Builds a view for a specific <see cref="ViewTargets"/> value with an optional
		/// parameter used to load the view.
		/// </summary>
		/// <param name="viewTarget">
		/// The <see cref="ViewTargets"/> value to build the view for.
		/// </param>
		/// <param name="viewParams">The parameter used to load the view.</param>
		/// <returns>Returns a <see cref="ViewResult"/> containing the view.</returns>
		ViewResult Build(ViewTargets viewTarget, Object viewParams);
	}
}
