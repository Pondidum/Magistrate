using System;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class ChangeRoleNameCommand : INotification
	{
		public Operator Operator { get; }
		public Guid RoleID { get; }
		public string NewName { get; }

		public ChangeRoleNameCommand(Operator @operator, Guid id, string newName)
		{
			Operator = @operator;
			RoleID = id;
			NewName = newName;
		}
	}
}
