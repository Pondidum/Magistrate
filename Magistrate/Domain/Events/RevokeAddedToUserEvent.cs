using System;

namespace Magistrate.Domain.Events
{
	public class RevokeAddedToUserEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
