using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmFabric.Messaging
{
	/// <summary>
	/// A message requesting a view to be shown.
	/// </summary>
	public sealed class ShowViewMessage
	{
		/// <summary>
		/// Gets the <see cref="ViewTargets"/> value indicating which view to show.
		/// </summary>
		public ViewTargets ViewTarget { get; private set; }

		/// <summary>
		/// Gets the optional load arguments used by the view to load itself.
		/// </summary>
		public Object LoadArgs { get; private set; }

		/// <summary>
		/// Constructor for ShowViewMessage.
		/// </summary>
		/// <param name="viewTarget">
		/// The <see cref="ViewTargets"/> value indicating which view to show.
		/// </param>
		public ShowViewMessage(ViewTargets viewTarget)
		{
			ViewTarget = viewTarget;
		}

		/// <summary>
		/// Constructor for ShowViewMessage which provides optional load arguments used by
		/// the view to load itself.
		/// </summary>
		/// <param name="viewTarget">
		/// The <see cref="ViewTargets"/> value indicating which view to show.
		/// </param>
		/// <param name="loadArgs">
		/// Optional load arguments which will be used by the view to load itself.
		/// </param>
		public ShowViewMessage(ViewTargets viewTarget, Object loadArgs)
			: this(viewTarget)
		{
			LoadArgs = loadArgs;
		}
	}
}
