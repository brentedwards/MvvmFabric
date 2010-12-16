using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using NSubstitute;
using MvvmFabric.Navigation;

namespace MvvmFabric.Test.Navigation
{
	[TestClass]
	public sealed class ViewFactoryTests
	{
		private IViewConfigurationResolver _viewConfigResolver;

		public interface IViewModelWithNoArgumentLoad
		{
			void Load();
		}

		public interface IViewModelWithParameterizedLoad
		{
			void Load(String parameter);
		}


		private ViewFactory GetViewFactory()
		{
			_viewConfigResolver = Substitute.For<IViewConfigurationResolver>();

			return new ViewFactory(_viewConfigResolver);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void BuildWithParamsThrowsExceptionWhenNoMatchingLoadFound()
		{
			var view = new FrameworkElement();
			var viewModel = Substitute.For<IViewModelWithNoArgumentLoad>();

			var viewFactory = GetViewFactory();

			_viewConfigResolver.ResolveConfiguration(ViewTargets.DefaultView)
				.Returns(new ViewConfiguration(view, viewModel));

			viewFactory.Build(ViewTargets.DefaultView, new Object());
		}

		[TestMethod, ExpectedException(typeof(NullReferenceException))]
		public void BuildThrowsExceptionWithViewAndNoViewModel()
		{
			var view = new FrameworkElement();

			var viewFactory = GetViewFactory();

			_viewConfigResolver.ResolveConfiguration(ViewTargets.DefaultView)
				.Returns(new ViewConfiguration(view));

			viewFactory.Build(ViewTargets.DefaultView);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void BuildWithParamsAndViewModelWithNoLoadMethodThrowsException()
		{
			var view = new FrameworkElement();
			var viewModel = new Object();
			
			var viewFactory = GetViewFactory();

			_viewConfigResolver.ResolveConfiguration(ViewTargets.DefaultView)
				.Returns(new ViewConfiguration(view, viewModel));

			viewFactory.Build(ViewTargets.DefaultView, Guid.NewGuid().ToString());
		}

		[TestMethod]
		public void BuildWithNoParamsAndViewModelWithNoLoadReturnsView()
		{
			var view = new FrameworkElement();
			var viewModel = Substitute.For<IViewModelWithNoArgumentLoad>();

			var viewFactory = GetViewFactory();

			_viewConfigResolver.ResolveConfiguration(ViewTargets.DefaultView)
				.Returns(new ViewConfiguration(view, viewModel));

			var viewResult = viewFactory.Build(ViewTargets.DefaultView);

			Assert.IsNotNull(viewResult);

			var resultViewModel = viewResult.View.DataContext;
			Assert.AreSame(viewModel, resultViewModel);
		}

		[TestMethod]
		public void BuildWithNoParamsAndViewModelWithLoadWithNoParamsReturnsView()
		{
			var view = new FrameworkElement();
			var viewModel = Substitute.For<IViewModelWithNoArgumentLoad>();

			var viewFactory = GetViewFactory();

			_viewConfigResolver.ResolveConfiguration(ViewTargets.DefaultView)
				.Returns(new ViewConfiguration(view, viewModel));

			var viewResult = viewFactory.Build(ViewTargets.DefaultView);

			Assert.IsNotNull(viewResult);

			var resultViewModel = viewResult.View.DataContext;
			Assert.AreSame(viewModel, resultViewModel);
			viewModel.Received().Load();
		}

		[TestMethod]
		public void BuildWithParamsReturnsControlAndLoadedViewModel()
		{
			var view = new FrameworkElement();
			var viewModel = Substitute.For<IViewModelWithParameterizedLoad>();

			var viewFactory = GetViewFactory();

			_viewConfigResolver.ResolveConfiguration(ViewTargets.DefaultView)
				.Returns(new ViewConfiguration(view, viewModel));

			var param = Guid.NewGuid().ToString();
			var viewResult = viewFactory.Build(ViewTargets.DefaultView, param);

			Assert.IsNotNull(viewResult);

			var resultViewModel = viewResult.View.DataContext;
			Assert.AreSame(viewModel, resultViewModel);
			viewModel.Received().Load(param);
		}

		[TestMethod]
		public void BuildWithImpliedViewModelReturnsControlAndLoadedViewModel()
		{
			var view = new FrameworkElement();
			var viewModel = Substitute.For<IViewModelWithNoArgumentLoad>();
			view.DataContext = viewModel;

			var viewFactory = GetViewFactory();

			_viewConfigResolver.ResolveConfiguration(ViewTargets.DefaultView)
				.Returns(new ViewConfiguration(view));

			var viewResult = viewFactory.Build(ViewTargets.DefaultView);

			Assert.IsNotNull(viewResult);

			var resultViewModel = viewResult.View.DataContext;
			Assert.AreSame(viewModel, resultViewModel);
			viewModel.Received().Load();
		}
	}
}
