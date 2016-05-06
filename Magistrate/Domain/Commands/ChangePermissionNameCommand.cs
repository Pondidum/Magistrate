using System;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class ChangePermissionNameCommand : INotification
	{
		public Operator Operator { get; }
		public Guid PermissionID { get; }
		public string NewName { get; }

		public ChangePermissionNameCommand(Operator @operator, Guid id, string newName)
		{
			Operator = @operator;
			PermissionID = id;
			NewName = newName;
		}
	}
}
