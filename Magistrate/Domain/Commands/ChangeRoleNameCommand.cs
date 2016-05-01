using System;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class ChangeRoleNameCommand : INotification
	{
		public Operator User { get; }
		public Guid RoleID { get; }
		public string NewName { get; }

		public ChangeRoleNameCommand(Operator user, Guid id, string newName)
		{
			User = user;
			RoleID = id;
			NewName = newName;
		}
	}
}
