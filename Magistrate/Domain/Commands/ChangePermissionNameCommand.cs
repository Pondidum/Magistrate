using System;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class ChangePermissionNameCommand : INotification
	{
		public Operator User { get; }
		public Guid PermissionID { get; }
		public string NewName { get; }

		public ChangePermissionNameCommand(Operator user, Guid id, string newName)
		{
			User = user;
			PermissionID = id;
			NewName = newName;
		}
	}
}
