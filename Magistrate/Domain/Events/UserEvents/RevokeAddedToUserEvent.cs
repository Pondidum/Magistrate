using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class RevokeAddedToUserEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
