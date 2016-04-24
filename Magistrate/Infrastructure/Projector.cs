using System;
using System.Collections.Generic;
using Ledger;

namespace Magistrate.Infrastructure
{
	public class Projector
	{
		private readonly Dictionary<Type, Action<DomainEvent<Guid>>> _projections;

		public Projector()
		{
			_projections = new Dictionary<Type, Action<DomainEvent<Guid>>>();
		}

		public void Register<TEvent>(Action<TEvent> projection) where TEvent : DomainEvent<Guid>
		{
			_projections[typeof(TEvent)] = e => projection((TEvent)e);
		}

		public void Apply(DomainEvent<Guid> e)
		{
			Action<DomainEvent<Guid>> projection;

			if (_projections.TryGetValue(e.GetType(), out projection))
				projection(e);
		}
	}
}
