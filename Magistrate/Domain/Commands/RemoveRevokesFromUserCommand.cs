using System;
using System.Collections.Generic;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class RemoveRevokesFromUserCommand : INotification
	{
		public Operator Operator { get; }
		public Guid UserID { get; }
		public IEnumerable<Guid> PermissionIDs { get; }

		public RemoveRevokesFromUserCommand(Operator @operator, Guid userID, IEnumerable<Guid> permissionIDs)
		{
			Operator = @operator;
			UserID = userID;
			PermissionIDs = permissionIDs;
		}
	}
}
