using System;
using System.Collections.Generic;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class RemoveRolesFromUserCommand : INotification
	{
		public Operator Operator { get; }
		public Guid UserID { get; }
		public IEnumerable<Guid> RoleIDs { get; }

		public RemoveRolesFromUserCommand(Operator @operator, Guid userID, IEnumerable<Guid> roles)
		{
			Operator = @operator;
			UserID = userID;
			RoleIDs = roles;
		}
	}
}
