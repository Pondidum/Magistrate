using System;
using System.Collections.Generic;
using System.Linq;
using Ledger;

namespace Magistrate.Infrastructure
{
	public class Projector
	{
		private readonly Dictionary<Type, List<Action<DomainEvent<Guid>>>> _projections;

		public Projector()
		{
			_projections = new Dictionary<Type, List<Action<DomainEvent<Guid>>>>();
		}

		public void Register<TEvent>(Action<TEvent> projection) where TEvent : DomainEvent<Guid>
		{
			List<Action<DomainEvent<Guid>>> handlers;

			if (_projections.TryGetValue(typeof(TEvent), out handlers) == false)
			{
				handlers = new List<Action<DomainEvent<Guid>>>();
				_projections.Add(typeof(TEvent), handlers);
			}

			handlers.Add(e => projection((TEvent)e));
		}

		public void Apply(DomainEvent<Guid> @event)
		{
			var handlers = _projections
				 .Where(pair => pair.Key.IsInstanceOfType(@event))
				 .SelectMany(pair => pair.Value);

			foreach (var projection in handlers)
				projection(@event);
		}
	}
}
