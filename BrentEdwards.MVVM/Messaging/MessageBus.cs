using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrentEdwards.MVVM.Messaging
{
	public sealed class MessageBus : IMessageBus
	{
		private Dictionary<Type, List<ActionReference>> _subscribers =
			new Dictionary<Type, List<ActionReference>>();

		private object _lock = new object();

		public void Subscribe<TMessage>(Action<TMessage> handler)
		{
			lock (_lock)
			{
				if (_subscribers.ContainsKey(typeof(TMessage)))
				{
					var handlers = _subscribers[typeof(TMessage)];
					handlers.Add(new ActionReference(handler));
				}
				else
				{
					var handlers = new List<ActionReference>();
					handlers.Add(new ActionReference(handler));
					_subscribers[typeof(TMessage)] = handlers;
				}
			}
		}

		public void Unsubscribe<TMessage>(Action<TMessage> handler)
		{
			lock (_lock)
			{
				if (_subscribers.ContainsKey(typeof(TMessage)))
				{
					var handlers = _subscribers[typeof(TMessage)];

					ActionReference targetReference = null;
					foreach (var reference in handlers)
					{
						if (((Action<TMessage>)reference.Target).Method.Equals(handler.Method))
						{
							targetReference = reference;
							break;
						}
					}
					handlers.Remove(targetReference);

					if (handlers.Count == 0)
					{
						_subscribers.Remove(typeof(TMessage));
					}
				}
			}
		}

		public void Publish<TMessage>(TMessage message)
		{
			var subscribers = RefreshAndGetSubscribers<TMessage>();
			foreach (var subscriber in subscribers)
			{
				subscriber.Invoke(message);
			}
		}

		private List<Action<TMessage>> RefreshAndGetSubscribers<TMessage>()
		{
			var toCall = new List<Action<TMessage>>();
			var toRemove = new List<ActionReference>();

			lock (_lock)
			{
				if (_subscribers.ContainsKey(typeof(TMessage)))
				{
					var handlers = _subscribers[typeof(TMessage)];
					foreach (var handler in handlers)
					{
						if (handler.IsAlive)
						{
							toCall.Add((Action<TMessage>)handler.Target);
						}
						else
						{
							toRemove.Add(handler);
						}
					}

					foreach (var remove in toRemove)
					{
						handlers.Remove(remove);
					}

					if (handlers.Count == 0)
					{
						_subscribers.Remove(typeof(TMessage));
					}
				}
			}

			return toCall;
		}
	}
}
