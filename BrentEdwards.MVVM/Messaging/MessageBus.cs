using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrentEdwards.MVVM.Messaging
{
	public sealed class MessageBus : IMessageBus
	{
		private Dictionary<Type, List<Object>> _subscribers = new Dictionary<Type, List<Object>>();

		public void Subscribe<TMessage>(Action<TMessage> handler)
		{
			if (_subscribers.ContainsKey(typeof(TMessage)))
			{
				var handlers = _subscribers[typeof(TMessage)];
				handlers.Add(handler);
			}
			else
			{
				var handlers = new List<Object>();
				handlers.Add(handler);
				_subscribers[typeof(TMessage)] = handlers;
			}
		}

		public void Unsubscribe<TMessage>(Action<TMessage> handler)
		{
			if (_subscribers.ContainsKey(typeof(TMessage)))
			{
				var handlers = _subscribers[typeof(TMessage)];
				handlers.Remove(handler);

				if (handlers.Count == 0)
				{
					_subscribers.Remove(typeof(TMessage));
				}
			}
		}

		public void Publish<TMessage>(TMessage message)
		{
			if (_subscribers.ContainsKey(typeof(TMessage)))
			{
				var handlers = _subscribers[typeof(TMessage)];
				foreach (Action<TMessage> handler in handlers)
				{
					handler.Invoke(message);
				}
			}
		}
	}
}
