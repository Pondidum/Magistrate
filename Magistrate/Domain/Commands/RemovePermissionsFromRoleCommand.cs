using System;
using System.Collections.Generic;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class RemovePermissionsFromRoleCommand : INotification
	{
		public Operator Operator { get; }
		public Guid RoleID { get; }
		public IEnumerable<Guid> PermissionIDs { get; }

		public RemovePermissionsFromRoleCommand(Operator user, Guid roleID, IEnumerable<Guid> permissionIDs)
		{
			Operator = user;
			RoleID = roleID;
			PermissionIDs = permissionIDs;
		}
	}
}
