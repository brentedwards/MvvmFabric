﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmFabric.Messaging;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using System.Windows;
using MvvmFabric.Navigation;
using NSubstitute;
using System.Windows.Controls;
using MvvmFabric.Movies.Core.Navigation;
using MvvmFabric.Movies.Core.ViewModels;
using MvvmFabric.Movies.Core.Messaging;

namespace MvvmFabric.Movies.Core.Test.Navigation
{
	[TestClass]
	public class ViewPlacerTests
	{
		private IMessageBus _MessageBus;

		private void CreateContainer()
		{
			var container = new WindsorContainer();
			ComponentContainer.Container = container;

			_MessageBus = Substitute.For<IMessageBus>();
			container.Register(Component.For<IMessageBus>().Instance(_MessageBus));
		}

		private void CreateContainerWithRealMessageBus()
		{
			var container = new WindsorContainer();
			ComponentContainer.Container = container;

			_MessageBus = new MessageBus();
			container.Register(Component.For<IMessageBus>().Instance(_MessageBus));
		}

		[TestMethod]
		public void ShowView()
		{
			CreateContainer();

			var title = Guid.NewGuid().ToString();
			
			var viewModel = Substitute.For<ITitledViewModel>();
			viewModel.Title.Returns(title);
			
			var view = new FrameworkElement();
			view.DataContext = viewModel;
			var viewTarget = ViewTargets.DefaultView;

			var viewResult = new ViewResult(view, viewTarget);

			var window = new Window();
			var tabControl = new TabControl();
			var viewPlacer = new ViewPlacer(window, tabControl);

			viewPlacer.PlaceView(viewResult);

			var viewFound = false;
			foreach (TabItem tabItem in tabControl.Items)
			{
				if (tabItem.Header.ToString() == title)
				{
					viewFound = true;
				}
			}

			Assert.IsTrue(viewFound);
		}

		[TestMethod]
		public void ShowViewExists()
		{
			CreateContainer();
			
			var title = Guid.NewGuid().ToString();

			var viewModel = Substitute.For<ITitledViewModel>();
			viewModel.Title.Returns(title);

			var view = new FrameworkElement();
			view.DataContext = viewModel;
			var viewTarget = ViewTargets.DefaultView;

			var viewResult = new ViewResult(view, viewTarget);

			var window = new Window();
			var tabControl = new TabControl();
			var viewPlacer = new ViewPlacer(window, tabControl);
			var newTabItem = new TabItem() { Header = title };
			tabControl.Items.Add(newTabItem);

			viewPlacer.PlaceView(viewResult);

			var viewsFound = 0;
			foreach (TabItem tabItem in tabControl.Items)
			{
				if (tabItem.Header.ToString() == title)
				{
					viewsFound++;
				}
			}

			Assert.IsTrue(viewsFound == 1);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void ShowView_InvalidViewModel()
		{
			CreateContainer();

			var viewModel = new object();

			var view = new FrameworkElement();
			view.DataContext = viewModel;
			var viewTarget = ViewTargets.DefaultView;

			var viewResult = new ViewResult(view, viewTarget);

			var window = new Window();
			var tabControl = new TabControl();
			var viewPlacer = new ViewPlacer(window, tabControl);

			viewPlacer.PlaceView(viewResult);
		}

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

			public void OnRequestClose(object sender, RequestCloseEventArgs e)
			{
				throw new NotImplementedException();
			}
		}

		[TestMethod]
		public void ShowView_AdvancedSearch()
		{
			CreateContainer();

			var title = Guid.NewGuid().ToString();

			var viewModel = Substitute.For<IModalViewModel>();

			var view = new MockModalView();
			view.DataContext = viewModel;
			var viewTarget = MoviesViewTargets.AdvancedSearch;

			var viewResult = new ViewResult(view, viewTarget);

			var window = new Window();
			var tabControl = new TabControl();
			var viewPlacer = new ViewPlacer(window, tabControl);

			viewPlacer.PlaceView(viewResult);

			Assert.IsTrue(view.ShowModalCalled);
			_MessageBus.Received().Publish<ModalViewClosedMessage>(Arg.Any<ModalViewClosedMessage>());
		}

		[TestMethod]
		public void CloseExists()
		{
			CreateContainerWithRealMessageBus();
			
			var title = Guid.NewGuid().ToString();

			var viewModel = Substitute.For<ITitledViewModel>();
			viewModel.Title.Returns(title);
			
			var view = new FrameworkElement();
			view.DataContext = viewModel;
			var viewTarget = ViewTargets.DefaultView;

			var viewResult = new ViewResult(view, viewTarget);
			var viewBuilder = Substitute.For<IViewFactory>();
			viewBuilder.Build(Arg.Any<ViewTargets>(), Arg.Any<Object>())
				.Returns(viewResult);
			ComponentContainer.Container.Register(Component.For<IViewFactory>().Instance(viewBuilder));

			var window = new Window();
			var tabControl = new TabControl();
			var viewController = new ViewPlacer(window, tabControl);
			var newTabItem = new TabItem() { Header = title };
			tabControl.Items.Add(newTabItem);

			var message = new CloseViewMessage(title);
			_MessageBus.Publish<CloseViewMessage>(message);

			Assert.AreEqual(0, tabControl.Items.Count);
		}

		[TestMethod]
		public void CloseDoesNotExist()
		{
			CreateContainerWithRealMessageBus();
			
			var title = Guid.NewGuid().ToString();

			var viewModel = Substitute.For<ITitledViewModel>();
			viewModel.Title.Returns(title);

			var view = new FrameworkElement();
			view.DataContext = viewModel;
			var viewTarget = ViewTargets.DefaultView;

			var viewResult = new ViewResult(view, viewTarget);
			var viewBuilder = Substitute.For<IViewFactory>();
			viewBuilder.Build(Arg.Any<ViewTargets>(), Arg.Any<Object>())
				.Returns(viewResult);
			ComponentContainer.Container.Register(Component.For<IViewFactory>().Instance(viewBuilder));

			var window = new Window();
			var tabControl = new TabControl();
			var viewController = new ViewPlacer(window, tabControl);
			var newTabItem = new TabItem() { Header = Guid.NewGuid().ToString() };
			tabControl.Items.Add(newTabItem);

			var message = new CloseViewMessage(title);
			_MessageBus.Publish<CloseViewMessage>(message);

			Assert.AreEqual(1, tabControl.Items.Count);
		}
	}
}
