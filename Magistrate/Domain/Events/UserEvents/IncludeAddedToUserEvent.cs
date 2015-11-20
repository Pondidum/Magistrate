using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class IncludeAddedToUserEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
