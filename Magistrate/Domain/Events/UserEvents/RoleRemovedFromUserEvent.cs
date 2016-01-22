using System;

namespace Magistrate.Domain.Events.UserEvents
{
	public class RoleRemovedFromUserEvent : UserLoggedEvent
	{
		public RoleRemovedFromUserEvent(MagistrateUser user, Guid roleID, string roleName, string userName) : base(user)
		{
			RoleID = roleID;
			RoleName = roleName;
			UserName = userName;
		}

		public Guid RoleID { get; }
		public string RoleName { get; }
		public string UserName { get; }
		public override string EventDescription => $"Removed '{RoleName}' from {UserName}'s Roles";
	}
}
