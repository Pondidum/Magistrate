using System;

namespace Magistrate.Domain.Events
{
	public class UserIncludeAddedEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
