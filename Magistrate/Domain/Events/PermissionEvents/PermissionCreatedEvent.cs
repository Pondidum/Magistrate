using System;

namespace Magistrate.Domain.Events.PermissionEvents
{
	public class PermissionCreatedEvent : UserLoggedEvent
	{
		public Guid ID { get; }
		public PermissionKey Key { get; }
		public string Name { get; }
		public string Description { get; }

		public PermissionCreatedEvent(Operator @operator, Guid id, PermissionKey key, string name, string description)
			: base(@operator)
		{
			ID = id;
			Key = key;
			Name = name;
			Description = description;
		}
	}
}
