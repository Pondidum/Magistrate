using System;

namespace Magistrate.Domain.Events.RoleEvents
{
	public class RoleCreatedEvent : UserLoggedEvent
	{
		public Guid ID { get; set; }
		public RoleKey Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
