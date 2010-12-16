using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using MvvmFabric.Navigation;

namespace MvvmFabric.Test
{
	[TestClass]
	public class ModalViewModelBaseTests
	{
		private bool _accepted;
		private object _viewResult;
		private void modalViewModel_RequestClose(object sender, RequestCloseEventArgs e)
		{
			_accepted = e.Accepted;
			_viewResult = e.ViewResult;
		}

		[TestMethod]
		public void NotifyCloseRequest()
		{
			_accepted = false;
			_viewResult = null;

			var modalViewModel = Substitute.For<ModalViewModelBase>();

			modalViewModel.RequestClose += modalViewModel_RequestClose;
			modalViewModel.NotifyCloseRequest();
			modalViewModel.RequestClose -= modalViewModel_RequestClose;

			Assert.IsTrue(_accepted);
			Assert.IsNull(_viewResult);
		}

		[TestMethod]
		public void NotifyCloseRequest_ExplicitAccepted()
		{
			_accepted = false;
			_viewResult = null;

			var modalViewModel = Substitute.For<ModalViewModelBase>();

			modalViewModel.RequestClose += modalViewModel_RequestClose;
			modalViewModel.NotifyCloseRequest(true);
			modalViewModel.RequestClose -= modalViewModel_RequestClose;

			Assert.IsTrue(_accepted);
			Assert.IsNull(_viewResult);
		}

		[TestMethod]
		public void NotifyCloseRequest_ExplicitAccepted_WithViewResult()
		{
			_accepted = false;
			_viewResult = null;

			var expectedViewResult = new object();

			var modalViewModel = Substitute.For<ModalViewModelBase>();

			modalViewModel.RequestClose += modalViewModel_RequestClose;
			modalViewModel.NotifyCloseRequest(true, expectedViewResult);
			modalViewModel.RequestClose -= modalViewModel_RequestClose;

			Assert.IsTrue(_accepted);
			Assert.AreSame(expectedViewResult, _viewResult);
		}
	}
}
