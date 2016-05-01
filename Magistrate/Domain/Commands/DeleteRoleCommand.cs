using System;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class DeleteRoleCommand : INotification
	{
		public Operator User { get; }
		public Guid RoleID { get; }

		public DeleteRoleCommand(Operator user, Guid roleID)
		{
			User = user;
			RoleID = roleID;
		}
	}
}
