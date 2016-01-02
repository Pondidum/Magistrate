using System;
using System.Collections.Generic;
using System.Linq;
using Ledger;

namespace Magistrate.Domain.Services
{
	public class Projector
	{
		private readonly Dictionary<Type, List<Action<IDomainEvent<Guid>>>> _handlers;

		public Projector()
		{
			_handlers = new Dictionary<Type, List<Action<IDomainEvent<Guid>>>>();
		}

		public void Add<TEvent>(Action<TEvent> handler)
		{
			List<Action<IDomainEvent<Guid>>> handlers;

			if (_handlers.TryGetValue(typeof (TEvent), out handlers) == false)
			{
				handlers = new List<Action<IDomainEvent<Guid>>>();
				_handlers.Add(typeof(TEvent), handlers);
			}

			handlers.Add(e => handler((TEvent)e));
		}

		public void Project(IDomainEvent<Guid> @event)
		{
			var handlers = _handlers
				.Where(pair => pair.Key.IsAssignableFrom(@event.GetType()))
				.SelectMany(pair => pair.Value);

			foreach (var handler in handlers)
			{
				handler(@event);
			}
		}
	}
}
