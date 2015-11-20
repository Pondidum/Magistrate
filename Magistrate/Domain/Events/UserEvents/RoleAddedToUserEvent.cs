using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class RoleAddedToUserEvent : UserLoggedEvent
	{
		 public Guid RoleID { get; set; }
	}
}
