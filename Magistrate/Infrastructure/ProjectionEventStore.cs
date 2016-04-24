using System;
using System.Collections.Generic;
using Ledger;
using Ledger.Infrastructure;
using Ledger.Stores;

namespace Magistrate.Infrastructure
{
	public class ProjectionStore : InterceptingEventStore
	{
		private readonly Action<DomainEvent<Guid>> _projection;

		public ProjectionStore(IEventStore other, Action<DomainEvent<Guid>> projection) : base(other)
		{
			_projection = projection;
		}

		public override IStoreWriter<TKey> CreateWriter<TKey>(EventStoreContext context)
		{
			var other = base.CreateWriter<TKey>(context);

			return new AsyncWriter<TKey>(other, e => _projection(e as DomainEvent<Guid>));
		}

		private class AsyncWriter<T> : InterceptingWriter<T>
		{
			private readonly Action<DomainEvent<T>> _projection;

			public AsyncWriter(IStoreWriter<T> other, Action<DomainEvent<T>> projection) : base(other)
			{
				_projection = projection;
			}

			public override void SaveEvents(IEnumerable<DomainEvent<T>> changes)
			{
				var toRaise = new List<DomainEvent<T>>();

				base.SaveEvents(changes.Apply(e => toRaise.Add(e)));

				foreach (var domainEvent in toRaise)
					_projection(domainEvent);
			}
		}
	}
}
