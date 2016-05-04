using System;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class DeleteRoleCommand : INotification
	{
		public Operator Operator { get; }
		public Guid RoleID { get; }

		public DeleteRoleCommand(Operator @operator, Guid roleID)
		{
			Operator = @operator;
			RoleID = roleID;
		}
	}
}
