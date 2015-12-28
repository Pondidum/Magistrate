using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class UserCreatedEvent : UserLoggedEvent
	{
		public Guid ID { get; set; }
		public UserKey Key { get; set; }
		public string Name { get; set; }
	}
}
