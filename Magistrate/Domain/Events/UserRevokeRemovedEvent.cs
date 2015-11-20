using System;

namespace Magistrate.Domain.Events
{
	public class UserRevokeRemovedEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
