using System;
using Magistrate.Domain.Events;

namespace Magistrate.ReadModels
{
	public class HistoryEntry
	{
		public DateTime Stamp { get; }
		public string Description { get; }

		public HistoryEntry(UserLoggedEvent e, string description)
		{
			Stamp = e.Stamp;
			Description = description;
		}
	}
}
