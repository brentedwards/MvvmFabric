using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmFabric.Navigation
{
	/// <summary>
	/// Event Args for when a view is requested to be closed.
	/// </summary>
	public sealed class RequestCloseEventArgs : EventArgs
	{
		/// <summary>
		/// Gets whether the view was accepted or cancelled.
		/// </summary>
		public bool Accepted { get; private set; }

		/// <summary>
		/// Gets the optional result returned by the view.
		/// </summary>
		public object ViewResult { get; private set; }

		/// <summary>
		/// Constructor for RequestCloseEventArgs.
		/// </summary>
		/// <param name="accepted">
		/// Indicates whether the view was accepted or cancelled.
		/// </param>
		public RequestCloseEventArgs(bool accepted)
		{
			Accepted = accepted;
		}

		/// <summary>
		/// Constructor for RequestCloseEventArgs which takes an optional view result.
		/// </summary>
		/// <param name="accepted">
		/// Indicates whether the view was accepted or cancelled.
		/// </param>
		/// <param name="viewResult">
		/// The result returned by the view.
		/// </param>
		public RequestCloseEventArgs(bool accepted, object viewResult)
			: this(accepted)
		{
			ViewResult = viewResult;
		}
	}
}
