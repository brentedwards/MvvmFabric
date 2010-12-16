using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmFabric.Messaging;

namespace MvvmFabric.Test.Messaging
{
	[TestClass]
	public class ModalViewClosedMessageTests
	{
		[TestMethod]
		public void Create()
		{
			var viewTarget = ViewTargets.DefaultView;
			var accepted = true;

			var message = new ModalViewClosedMessage(viewTarget, accepted);

			Assert.AreEqual(viewTarget, message.ViewTarget, "ViewTarget");
			Assert.AreEqual(accepted, message.Accepted, "Accepted");
		}

		[TestMethod]
		public void Create_WithViewResult()
		{
			var viewTarget = ViewTargets.DefaultView;
			var accepted = true;
			var viewResult = new object();

			var message = new ModalViewClosedMessage(viewTarget, accepted, viewResult);

			Assert.AreEqual(viewTarget, message.ViewTarget, "ViewTarget");
			Assert.AreEqual(accepted, message.Accepted, "Accepted");
			Assert.AreSame(viewResult, message.ViewResult);
		}
	}
}
