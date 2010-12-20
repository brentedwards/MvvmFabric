using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmFabric.Navigation;
using System.Windows;

namespace MvvmFabric.Test.Navigation
{
	[TestClass]
	public class ViewConfigurationResolverTests
	{
		[TestMethod]
		public void Resolve_JustView()
		{
			var resolver = new ViewConfigurationResolver();

			resolver.RegisterViewConfiguration<FrameworkElement>(ViewTargets.DefaultView);

			var viewConfig = resolver.ResolveConfiguration(ViewTargets.DefaultView);

			Assert.IsInstanceOfType(viewConfig.View, typeof(FrameworkElement));
		}

		[TestMethod]
		public void Resolve_ViewAndViewModel()
		{
			var resolver = new ViewConfigurationResolver();

			resolver.RegisterViewConfiguration<FrameworkElement, int>(ViewTargets.DefaultView);

			var viewConfig = resolver.ResolveConfiguration(ViewTargets.DefaultView);

			Assert.IsInstanceOfType(viewConfig.View, typeof(FrameworkElement), "View");
			Assert.IsInstanceOfType(viewConfig.ViewModel, typeof(int), "View");
		}
	}
}
