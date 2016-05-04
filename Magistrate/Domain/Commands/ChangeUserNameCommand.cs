using System;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class ChangeUserNameCommand : INotification
	{
		public Operator Operator { get; }
		public Guid UserID { get; }
		public string NewName { get; }

		public ChangeUserNameCommand(Operator user, Guid id, string newName)
		{
			Operator = user;
			UserID = id;
			NewName = newName;
		}
	}
}
