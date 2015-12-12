using System;
using System.Collections.Generic;
using System.Linq;
using Ledger;

namespace Magistrate.Domain.Services
{
	public class Projector
	{
		private readonly Dictionary<Type, Action<IDomainEvent<Guid>>> _handlers;

		public Projector()
		{
			_handlers = new Dictionary<Type, Action<IDomainEvent<Guid>>>();
		}

		public void Add<TEvent>(Action<TEvent> handler)
		{
			_handlers.Add(typeof(TEvent), e => handler((TEvent)e));
		}

		public void Project(IDomainEvent<Guid> @event)
		{
			var handlers = _handlers
				.Where(pair => pair.Key == @event.GetType());

			foreach (var handler in handlers)
			{
				handler.Value(@event);
			}
		}
	}
}
