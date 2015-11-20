using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class UserCreatedEvent : UserLoggedEvent
	{
		public Guid ID { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
	}
}
