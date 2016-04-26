using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class IncludeRemovedFromUserEvent : UserLoggedEvent
	{
		public IncludeRemovedFromUserEvent(Operator user, Guid permissionID, string permissionName, string userName) : base(user)
		{
			PermissionID = permissionID;
			PermissionName = permissionName;
			UserName = userName;
		}

		public Guid PermissionID { get; }
		public string PermissionName { get; }
		public string UserName { get; }
		public override string EventDescription => $"Removed '{PermissionName}' from {UserName}'s Includes";
	}
}
