using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using BrentEdwards.MVVM.Navigation;
using BrentEdwards.MVVM.Messaging;
using NSubstitute;

namespace BrentEdwards.MVVM.Test.Navigation
{
	[TestClass]
	public class ViewControllerTests
	{
		private IMessageBus _messageBus;
		private IViewFactory _viewFactory;
		private IViewPlacer _viewPlacer;
		private IViewAuthorizer _viewAuthorizer;

		private ViewController GetViewController(bool useAuthorizer)
		{
			_messageBus = new MessageBus();
			_viewFactory = Substitute.For<IViewFactory>();
			_viewPlacer = Substitute.For<IViewPlacer>();

			if (useAuthorizer)
			{
				_viewAuthorizer = Substitute.For<IViewAuthorizer>();
			}
			else
			{
				_viewAuthorizer = null;
			}

			return new ViewController(_messageBus, _viewFactory, _viewPlacer, _viewAuthorizer);
		}

		[TestMethod]
		public void ShowView()
		{
			var view = new FrameworkElement();
			var viewController = GetViewController(false);
			
			var viewResult = new ViewResult(view, ViewTargets.DefaultView);
			_viewFactory.Build(Arg.Any<ViewTargets>(), Arg.Any<Object>())
				.Returns(viewResult);

			var message = new ShowViewMessage(ViewTargets.DefaultView);
			_messageBus.Publish<ShowViewMessage>(message);

			_viewPlacer.Received().PlaceView(viewResult);
		}

		[TestMethod]
		public void ShowViewAuthorized()
		{
			var view = new FrameworkElement();
			var viewController = GetViewController(true);

			var viewResult = new ViewResult(view, ViewTargets.DefaultView);
			_viewFactory.Build(Arg.Any<ViewTargets>(), Arg.Any<Object>())
				.Returns(viewResult);

			_viewAuthorizer.AuthorizeView(Arg.Any<ViewTargets>())
				.Returns(true);

			var message = new ShowViewMessage(ViewTargets.DefaultView);
			_messageBus.Publish<ShowViewMessage>(message);

			_viewPlacer.Received().PlaceView(viewResult);
		}

		[TestMethod, ExpectedException(typeof(InvalidOperationException))]
		public void ShowViewNotAuthorized()
		{
			var view = new FrameworkElement();
			var viewController = GetViewController(true);

			var viewResult = new ViewResult(view, ViewTargets.DefaultView);
			_viewFactory.Build(Arg.Any<ViewTargets>(), Arg.Any<Object>())
				.Returns(viewResult);

			_viewAuthorizer.AuthorizeView(Arg.Any<ViewTargets>())
				.Returns(false);

			var message = new ShowViewMessage(ViewTargets.DefaultView);
			_messageBus.Publish<ShowViewMessage>(message);
		}
	}
}
