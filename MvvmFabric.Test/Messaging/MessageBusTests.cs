using System;
using System.Collections.Generic;
using MvvmFabric.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MvvmFabric.Test.Messaging
{
	[TestClass]
	public sealed class MessageBusTests
	{
		private List<Object> _Messages;
		private void Handler(Object message)
		{
			_Messages.Add(message);
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_Messages = new List<Object>();
		}

		[TestMethod]
		public void SubscribeNotExisting()
		{
			var bus = new MessageBus();
			bus.Subscribe<Object>(Handler);

			var message = new Object();
			bus.Publish<Object>(message);

			Assert.AreEqual(1, _Messages.Count);
			Assert.AreSame(message, _Messages[0]);
		}

		[TestMethod]
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

		[TestMethod]
		public void Unsubscribe()
		{
			var bus = new MessageBus();
			bus.Subscribe<Object>(Handler);

			bus.Unsubscribe<Object>(Handler);

			var message = new Object();
			bus.Publish<Object>(message);

			Assert.AreEqual(0, _Messages.Count);
		}

		private class TestSubscriber
		{
			public List<Object> Messages { get; set; }

			public TestSubscriber()
			{
				Messages = new List<Object>();
			}

			public void Handler(Object message)
			{
				Messages.Add(message);
			}
		}

		[TestMethod]
		public void Unsubscribe_MultipleInstances_SameClass()
		{
			var bus = new MessageBus();

			// Subscribe two objects of the same type.
			var subscriber1 = new TestSubscriber();
			bus.Subscribe<Object>(subscriber1.Handler);

			var subscriber2 = new TestSubscriber();
			bus.Subscribe<Object>(subscriber2.Handler);

			// Unsubscribe the second of the objects and make sure the other first one still gets messages.
			bus.Unsubscribe<Object>(subscriber2.Handler);

			var message = new Object();
			bus.Publish<Object>(message);

			Assert.AreEqual(0, subscriber2.Messages.Count);
			Assert.AreEqual(1, subscriber1.Messages.Count);
		}

		[TestMethod]
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
