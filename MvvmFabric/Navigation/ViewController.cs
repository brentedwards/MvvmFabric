using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFabric.Messaging;

namespace MvvmFabric.Navigation
{
	/// <summary>
	/// The ViewController listens for requests to show views and facilitates creating,
	/// loading and showing views when a request is received.
	/// </summary>
	public sealed class ViewController
	{
		private IMessageBus MessageBus { get; set; }
		private IViewFactory ViewFactory { get; set; }
		private IViewPlacer ViewPlacer { get; set; }
		private IViewAuthorizer ViewAuthorizer { get; set; }

		/// <summary>
		/// Constructor for ViewController.
		/// </summary>
		/// <param name="messageBus">
		/// The <see cref="IMessageBus"/> which will be used to listen for view requests.
		/// </param>
		/// <param name="viewFactory">
		/// The <see cref="IViewFactory"/> which will be used to build the views.
		/// </param>
		/// <param name="viewPlacer">
		/// The <see cref="IViewPlacer"/> which will be used to place the views.
		/// </param>
		public ViewController(IMessageBus messageBus, IViewFactory viewFactory, IViewPlacer viewPlacer)
		{
			MessageBus = messageBus;
			ViewFactory = viewFactory;
			ViewPlacer = viewPlacer;

			Initialize();
		}

		/// <summary>
		/// Constructor for ViewController which takes an optional <see cref="IViewAuthorizer"/>.
		/// </summary>
		/// <param name="messageBus">
		/// The <see cref="IMessageBus"/> which will be used to listen for view requests.
		/// </param>
		/// <param name="viewFactory">
		/// The <see cref="IViewFactory"/> which will be used to build the views.
		/// </param>
		/// <param name="viewPlacer">
		/// The <see cref="IViewPlacer"/> which will be used to place the views.
		/// </param>
		/// <param name="viewAuthorizer">
		/// The <see cref="IViewAuthorizer"/> which will be used to authorize views.
		/// </param>
		public ViewController(IMessageBus messageBus, IViewFactory viewFactory, IViewPlacer viewPlacer, IViewAuthorizer viewAuthorizer)
			: this(messageBus, viewFactory, viewPlacer)
		{
			ViewAuthorizer = viewAuthorizer;
		}

		private void Initialize()
		{
			MessageBus.Subscribe<ShowViewMessage>(HandleShowView);
		}

		private Action<ShowViewMessage> ShowViewAction { get; set; }
		private void HandleShowView(ShowViewMessage args)
		{
			var viewauthorization = ViewAuthorizations.Authorized;
			if (ViewAuthorizer != null)
			{
				viewauthorization = ViewAuthorizer.AuthorizeView(args.ViewTarget);
			}

			if (viewauthorization == ViewAuthorizations.Authorized)
			{
				ShowView(args);
			}
			else if (viewauthorization == ViewAuthorizations.NotAuthorized)
			{
				throw new InvalidOperationException(string.Format("Not authorized to view '{0}'.", args.ViewTarget.ToString()));
			}
		}

		private void ShowView(ShowViewMessage args)
		{
			var viewResult = ViewFactory.Build(args.ViewTarget, args.LoadArgs);

			ViewPlacer.PlaceView(viewResult);
		}
	}
}
