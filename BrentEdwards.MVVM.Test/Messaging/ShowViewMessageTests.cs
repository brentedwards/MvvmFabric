using System;
using BrentEdwards.MVVM.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrentEdwards.MVVM.Test.Messaging
{
	[TestClass]
	public sealed class ShowViewMessageTests
	{
		[TestMethod()]
		public void CreateWithViewTarget()
		{
			var viewTarget = ViewTargets.DefaultView;

			var message = new ShowViewMessage(viewTarget);

			Assert.AreEqual(viewTarget, message.ViewTarget);
			Assert.IsNull(message.LoadArgs);
		}

		[TestMethod()]
		public void Create()
		{
			var viewTarget = ViewTargets.DefaultView;
			var loadArgs = new Object();

			var message = new ShowViewMessage(viewTarget, loadArgs);

			Assert.AreEqual(viewTarget, message.ViewTarget);
			Assert.AreSame(loadArgs, message.LoadArgs);
		}
	}
}
