using System;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class DeletePermissionCommand : INotification
	{
		public Operator User { get; }
		public Guid PermissionID { get; }

		public DeletePermissionCommand(Operator user, Guid permissionID)
		{
			User = user;
			PermissionID = permissionID;
		}
	}
}
