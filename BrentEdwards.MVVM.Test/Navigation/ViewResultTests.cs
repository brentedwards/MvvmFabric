using System.Windows;
using BrentEdwards.MVVM.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrentEdwards.MVVM.Test.Navigation
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
