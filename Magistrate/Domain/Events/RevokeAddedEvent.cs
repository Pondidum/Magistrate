using System;

namespace Magistrate.Domain.Events
{
	public class RevokeAddedEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
