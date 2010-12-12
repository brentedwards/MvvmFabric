using System;
using System.Collections.Generic;
using BrentEdwards.MVVM.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrentEdwards.MVVM.Test.Messaging
{
	[TestClass]
	public sealed class MessageBusTests
	{
		private List<Object> _Messages;
		private void Handler(Object message)
		{
			_Messages.Add(message);
		}

		[TestInitialize()]
		public void TestInitialize()
		{
			_Messages = new List<Object>();
		}

		[TestMethod()]
		public void SubscribeNotExisting()
		{
			var bus = new MessageBus();
			bus.Subscribe<Object>(Handler);

			var message = new Object();
			bus.Publish<Object>(message);

			Assert.AreEqual(1, _Messages.Count);
			Assert.AreSame(message, _Messages[0]);
		}

		[TestMethod()]
		public void SubscribeExisting()
		{
			var bus = new MessageBus();
			bus.Subscribe<Object>(Handler);
			bus.Subscribe<Object>(Handler);

			var message = new Object();
			bus.Publish<Object>(message);

			Assert.AreEqual(2, _Messages.Count);
			Assert.AreSame(message, _Messages[0]);
			Assert.AreSame(message, _Messages[1]);
		}

		[TestMethod()]
		public void Unsubscribe()
		{
			var bus = new MessageBus();
			bus.Subscribe<Object>(Handler);

			bus.Unsubscribe<Object>(Handler);

			var message = new Object();
			bus.Publish<Object>(message);

			Assert.AreEqual(0, _Messages.Count);
		}

		[TestMethod()]
		public void PublishDifferent()
		{
			var bus = new MessageBus();
			bus.Subscribe<Object>(Handler);

			var message = Guid.NewGuid().ToString();
			bus.Publish<String>(message);

			Assert.AreEqual(0, _Messages.Count);
		}
	}
}
