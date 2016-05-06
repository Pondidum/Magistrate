using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class UserCreatedEvent : UserLoggedEvent
	{
		public UserCreatedEvent(Operator @operator, Guid id, UserKey key, string name) : base(@operator)
		{
			ID = id;
			Key = key;
			Name = name;
		}

		public Guid ID { get; }
		public UserKey Key { get; }
		public string Name { get; }

		public override string EventDescription => $"Created new User '{Name}'";
	}
}
