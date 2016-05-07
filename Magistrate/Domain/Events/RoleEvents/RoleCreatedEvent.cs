using System;

namespace Magistrate.Domain.Events.RoleEvents
{
	public class RoleCreatedEvent : UserLoggedEvent
	{
		public RoleCreatedEvent(Operator @operator, Guid id, RoleKey key, string name, string description) : base(@operator)
		{
			ID = id;
			Key = key;
			Name = name;
			Description = description;
		}

		public Guid ID { get; }
		public RoleKey Key { get; }
		public string Name { get; }
		public string Description { get; }
	}
}
