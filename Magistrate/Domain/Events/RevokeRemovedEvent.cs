using System;

namespace Magistrate.Domain.Events
{
	public class RevokeRemovedEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
