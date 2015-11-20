using System;

namespace Magistrate.Domain.Events
{
	public class UserRevokeAddedEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
