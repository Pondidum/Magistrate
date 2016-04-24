using System;
using System.Collections.Generic;
using Ledger;

namespace Magistrate.Infrastructure
{
	public class Projectionist
	{
		private readonly List<Action<DomainEvent<Guid>>> _projections;

		public Projectionist()
		{
			_projections = new List<Action<DomainEvent<Guid>>>();
		}

		public void Apply(DomainEvent<Guid> e)
		{
			_projections.ForEach(p => p(e));
		}

		public Projectionist Add(Action<DomainEvent<Guid>> action)
		{
			_projections.Add(action);
			return this;
		}
	}
}
