using System;
using System.Collections.Generic;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class AddIncludesToUserCommand : INotification
	{
		public Operator Operator { get; }
		public Guid UserID { get; }
		public IEnumerable<Guid> PermissionIDs { get; }

		public AddIncludesToUserCommand(Operator @operator, Guid userID, IEnumerable<Guid> permissionIDs)
		{
			Operator = @operator;
			PermissionIDs = permissionIDs;
			UserID = userID;
		}
	}
}
