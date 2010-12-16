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
			lock (_lock)
			{
				if (_subscribers.ContainsKey(typeof(TMessage)))
				{
					var references = _subscribers[typeof(TMessage)];
					foreach (var reference in references)
					{
						if (reference.IsAlive)
						{
							((Action<TMessage>)reference.Target).Invoke(message);
						}
					}
				}
			}
		}
	}
}
