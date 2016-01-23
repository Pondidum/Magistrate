using System;
using System.Text.RegularExpressions;
using Magistrate.Domain.Events;

namespace Magistrate.Domain.ReadModels
{
	public class HistoryEntry
	{
		private static readonly Regex SentenceCase = new Regex("(?<!(^|[A-Z]))(?=[A-Z])|(?<!^)(?=[A-Z][a-z])");
		private static readonly Regex RemoveEventSuffix = new Regex("(Event$)");

		public string Action { get; private set; }
		public Guid OnAggregate { get; private set; }
		public MagistrateUser By { get; private set; }
		public DateTime At { get; private set; }
		public string Description { get; private set; }

		public static HistoryEntry From(UserLoggedEvent @event)
		{
			return new HistoryEntry
			{
				Action = ActionFromEvent(@event.GetType().Name),
				At = @event.Stamp,
				By = @event.User,
				OnAggregate = @event.AggregateID,
				Description = @event.EventDescription
			};
		}

		public static string ActionFromEvent(string eventName)
		{
			return SentenceCase.Replace(RemoveEventSuffix.Replace(eventName, ""), " $0");
		}
	}
}
