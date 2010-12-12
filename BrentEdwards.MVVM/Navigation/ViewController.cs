using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrentEdwards.MVVM.Messaging;

namespace BrentEdwards.MVVM.Navigation
{
	public sealed class ViewController
	{
		private IViewPlacer ViewPlacer { get; set; }
		private IViewAuthorizer ViewAuthorizer { get; set; }
		private IMessageBus MessageBus { get; set; }
		private IViewFactory ViewFactory { get; set; }

		public ViewController(IMessageBus messageBus, IViewFactory viewFactory, IViewPlacer viewPlacer)
		{
			MessageBus = messageBus;
			ViewFactory = viewFactory;
			ViewPlacer = viewPlacer;

			Initialize();
		}

		public ViewController(IMessageBus messageBus, IViewFactory viewFactory, IViewPlacer viewPlacer, IViewAuthorizer viewAuthorizer)
			: this(messageBus, viewFactory, viewPlacer)
		{
			ViewAuthorizer = viewAuthorizer;
		}

		private void Initialize()
		{
			MessageBus.Subscribe<ShowViewMessage>(HandleShowView);
		}

		private void HandleShowView(ShowViewMessage args)
		{
			var canShowView = true;
			if (ViewAuthorizer != null)
			{
				canShowView = ViewAuthorizer.AuthorizeView(args.ViewTarget);
			}

			if (canShowView)
			{
				ShowView(args);
			}
			else
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
