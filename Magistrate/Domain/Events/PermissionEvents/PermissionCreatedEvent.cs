using System;

namespace Magistrate.Domain.Events.PermissionEvents
{
	public class PermissionCreatedEvent : UserLoggedEvent
	{
		public Guid ID { get; }
		public PermissionKey Key { get; }
		public string Name { get; }
		public string Description { get; }

		public PermissionCreatedEvent(Operator user, Guid id, PermissionKey key, string name, string description)
			: base(user)
		{
			ID = id;
			Key = key;
			Name = name;
			Description = description;
		}

		public override string EventDescription => $"Created new Permission '{Name}'";
	}
}
