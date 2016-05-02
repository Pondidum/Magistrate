using System;
using System.Collections.Generic;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class AddPermissionsToRoleCommand : INotification
	{
		public Operator Operator { get; set; }
		public Guid RoleID { get; set; }
		public IEnumerable<Guid> PermissionIDs { get; set; }

		public AddPermissionsToRoleCommand(Operator user, Guid roleID, IEnumerable<Guid> permissionIDs)
		{
			Operator = user;
			RoleID = roleID;
			PermissionIDs = permissionIDs;
		}
	}
}
