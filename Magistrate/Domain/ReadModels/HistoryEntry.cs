using System;
using Magistrate.Domain.Events;

namespace Magistrate.Domain.ReadModels
{
	public class HistoryEntry
	{
		public string Action { get; private set; }
		public Guid OnAggregate { get; private set; }
		public MagistrateUser By { get; private set; }
		public DateTime At { get; private set; }

		public static HistoryEntry From(UserLoggedEvent @event)
		{
			return new HistoryEntry
			{
				Action = @event.GetType().Name,
				At = @event.Stamp,
				By = @event.User,
				OnAggregate = @event.AggregateID
			};
		}
	}
}
