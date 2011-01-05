using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmFabric.Navigation
{
	/// <summary>
	/// Enumerates possible values for authorization results of views.
	/// </summary>
	public enum ViewAuthorizations
	{
		/// <summary>
		/// The user is authorized to see the view.
		/// </summary>
		Authorized,

		/// <summary>
		/// The user is not authorized to see the view.
		/// </summary>
		NotAuthorized,

		/// <summary>
		/// Do not display the view.
		/// </summary>
		DoNotDisplay
	}

	/// <summary>
	/// An interface for a View Authorizer.  A View Authorizer determines whether a view
	/// is authorized to be shown.
	/// </summary>
	public interface IViewAuthorizer
	{
		/// <summary>
		/// Determines whether the view is authorized to be shown.
		/// </summary>
		/// <param name="viewTarget">
		/// The <see cref="ViewTargets"/> value indicating which view to check authorization for.
		/// </param>
		/// <returns>Returns whether the requested view is authorized to be shown.</returns>
		ViewAuthorizations AuthorizeView(ViewTargets viewTarget);
	}
}
