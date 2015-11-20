using System;

namespace Magistrate.Domain.Events
{
	public class RevokeRemovedFromUserEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
