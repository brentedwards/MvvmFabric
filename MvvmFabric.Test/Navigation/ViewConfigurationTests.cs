using System;
using System.Windows;
using MvvmFabric.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MvvmFabric.Test.Navigation
{
	[TestClass]
	public sealed class ViewConfigurationTests
	{
		[TestMethod]
		public void Create()
		{
			var view = new FrameworkElement();

			var viewConfiguration = new ViewConfiguration(view);

			Assert.AreSame(view, viewConfiguration.View);
			Assert.IsNull(viewConfiguration.ViewModel);
		}

		[TestMethod]
		public void CreateWithViewModel()
		{
			var view = new FrameworkElement();
			var viewModel = new Object();

			var viewConfiguration = new ViewConfiguration(view, viewModel);

			Assert.AreSame(view, viewConfiguration.View);
			Assert.AreSame(viewModel, viewConfiguration.ViewModel);
		}
	}
}
