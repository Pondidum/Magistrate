using System;

namespace Magistrate.Domain.Events.PermissionEvents
{
	public class PermissionCreatedEvent : UserLoggedEvent
	{
		public Guid ID { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
