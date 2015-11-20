using System;

namespace Magistrate.Domain.Events
{
	public class UserIncludeRemovedEvent : UserLoggedEvent
	{
		public Guid PermissionID { get; set; }
	}
}
