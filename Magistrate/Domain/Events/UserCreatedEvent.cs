using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class UserCreatedEvent : DomainEvent
	{
		public Guid ID { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
	}
}
