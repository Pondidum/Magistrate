using System;

namespace Magistrate.Domain.Events.RoleEvents
{
	public class RoleCreatedEvent : UserLoggedEvent
	{
		public Guid ID { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
