using System;
using System.Collections.Generic;
using Ledger;
using Ledger.Infrastructure;
using Ledger.Stores;

namespace Magistrate.Infrastructure
{
	public class ProjectionEventStore : IEventStore
	{
		private readonly IEventStore _other;
		private readonly Action<IDomainEvent<Guid>> _onEvent;

		public ProjectionEventStore(IEventStore other, Action<IDomainEvent<Guid>> onEvent)
		{
			_other = other;
			_onEvent = onEvent;
		}

		public IStoreReader<TKey> CreateReader<TKey>(EventStoreContext context)
		{
			return _other.CreateReader<TKey>(context);
		}

		public IStoreWriter<TKey> CreateWriter<TKey>(EventStoreContext context)
		{
			var otherWriter = _other.CreateWriter<TKey>(context);

			return new ProjectionWriter<TKey>(otherWriter, e => _onEvent((IDomainEvent<Guid>)e));
		}

		private class ProjectionWriter<TKey> : InterceptingWriter<TKey>
		{
			private readonly Action<IDomainEvent<TKey>> _onEvent;

			public ProjectionWriter(IStoreWriter<TKey> other, Action<IDomainEvent<TKey>> onEvent)
				: base(other)
			{
				_onEvent = onEvent;
			}

			public override void SaveEvents(IEnumerable<IDomainEvent<TKey>> changes)
			{
				base.SaveEvents(changes.Apply(change => _onEvent(change)));
			}
		}
	}
}
