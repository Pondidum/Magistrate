using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class RevokeRemovedFromUserEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
