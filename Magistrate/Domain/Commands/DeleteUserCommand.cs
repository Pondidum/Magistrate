using System;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class DeleteUserCommand : INotification
	{
		public Operator Operator { get; }
		public Guid UserID { get; }

		public DeleteUserCommand(Operator user, Guid userID)
		{
			Operator = user;
			UserID = userID;
		}
	}
}
