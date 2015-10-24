using System;
using Ledger;

namespace Magistrate.Domain.Events
{
	public class RoleCreatedEvent : DomainEvent
	{
		public Guid ID { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
