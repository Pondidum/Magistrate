using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class UserCreatedEvent : UserLoggedEvent
	{
		public UserCreatedEvent(MagistrateUser user, Guid id, UserKey key, string name) : base(user)
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
