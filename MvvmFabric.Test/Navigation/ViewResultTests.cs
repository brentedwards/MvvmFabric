using System.Windows;
using MvvmFabric.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MvvmFabric.Test.Navigation
{
	[TestClass]
	public sealed class ViewResultTests
	{
		[TestMethod]
		public void Create()
		{
			var view = new FrameworkElement();

			var viewResult = new ViewResult(view, ViewTargets.DefaultView);

			Assert.AreSame(view, viewResult.View);
			Assert.AreEqual(ViewTargets.DefaultView, viewResult.ViewTarget);
		}
	}
}
