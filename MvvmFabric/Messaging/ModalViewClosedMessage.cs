using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmFabric.Messaging
{
	/// <summary>
	/// A message indicating that a modal view has been closed.
	/// </summary>
	public sealed class ModalViewClosedMessage
	{
		/// <summary>
		/// Gets the <see cref="ViewTargets" /> value indicating which view was closed.
		/// </summary>
		public ViewTargets ViewTarget { get; private set; }

		/// <summary>
		/// Gets whether the view was accepted or cancelled.
		/// </summary>
		public bool Accepted { get; private set; }

		/// <summary>
		/// Gets an optional result returned by the view being closed.
		/// </summary>
		public object ViewResult { get; private set; }

		/// <summary>
		/// Constructor for ModalViewClosedMessage.
		/// </summary>
		/// <param name="viewTarget">
		/// The <see cref="ViewTargets"/> value indicating which view was closed.
		/// </param>
		/// <param name="accepted">
		/// Indicates whether the view was accepted or cancelled.
		/// </param>
		public ModalViewClosedMessage(ViewTargets viewTarget, bool accepted)
		{
			ViewTarget = viewTarget;
			Accepted = accepted;
		}

		/// <summary>
		/// Constructor for ModalViewClosedMessage which specifies an optional result
		/// returned by the view.
		/// </summary>
		/// <param name="viewTarget">
		/// The <see cref="ViewTargets"/> value indicating which view was closed.
		/// </param>
		/// <param name="accepted">
		/// Indicates whether the view was accepted or cancelled.
		/// </param>
		/// <param name="viewResult">
		/// An optional result returned by the view.
		/// </param>
		public ModalViewClosedMessage(ViewTargets viewTarget, bool accepted, object viewResult)
			: this(viewTarget, accepted)
		{
			ViewResult = viewResult;
		}
	}
}
