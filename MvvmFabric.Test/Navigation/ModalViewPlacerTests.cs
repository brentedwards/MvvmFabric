using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using NSubstitute;
using MvvmFabric.Messaging;
using MvvmFabric.Navigation;

namespace MvvmFabric.Test.Navigation
{
	[TestClass]
	public class ModalViewPlacerTests
	{
		private class MockModalView : FrameworkElement, IModalView
		{

			public Window Owner { get; set; }
			public bool Accepted { get; set; }
			public object ViewResult { get; set; }

			public bool ShowModalCalled { get; set; }
			public void ShowModal()
			{
				ShowModalCalled = true;
			}

			public RequestCloseEventArgs CloseEventArgs { get; set; }
			public void OnRequestClose(object sender, RequestCloseEventArgs e)
			{
				CloseEventArgs = e;
			}
		}

		[TestMethod]
		public void PlaceView()
		{
			var appWindow = new Window();
			var messageBus = Substitute.For<IMessageBus>();
			
			var viewModel = Substitute.For<IModalViewModel>();
			var view = new MockModalView();
			view.DataContext = viewModel;

			var viewResult = new ViewResult(view, ViewTargets.DefaultView);

			var viewPlacer = new ModalViewPlacer(appWindow, messageBus);

			viewPlacer.PlaceView(viewResult);

			Assert.IsTrue(view.ShowModalCalled);
			messageBus.Received().Publish<ModalViewClosedMessage>(Arg.Any<ModalViewClosedMessage>());
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void PlaceView_ThrowsExceptionWithBadView()
		{
			var appWindow = new Window();
			var messageBus = Substitute.For<IMessageBus>();

			var view = new FrameworkElement();
			var viewResult = new ViewResult(view, ViewTargets.DefaultView);

			var viewPlacer = new ModalViewPlacer(appWindow, messageBus);

			viewPlacer.PlaceView(viewResult);
		}
	}
}
