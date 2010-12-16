using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmFabric.Messaging
{
	public interface IMessageBus
	{
		void Subscribe<TMessage>(Action<TMessage> handler);
		void Unsubscribe<TMessage>(Action<TMessage> handler);
		void Publish<TMessage>(TMessage message);
	}
}
