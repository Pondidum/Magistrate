using System;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class DeletePermissionCommand : INotification
	{
		public Operator Operator { get; }
		public Guid PermissionID { get; }

		public DeletePermissionCommand(Operator @operator, Guid permissionID)
		{
			Operator = @operator;
			PermissionID = permissionID;
		}
	}
}
