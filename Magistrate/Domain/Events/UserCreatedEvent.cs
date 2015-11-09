using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class UserCreatedEvent : UserLoggedEvent
	{
		public Guid ID { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
	}
}
