using System;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class ChangeRoleDescriptionCommand : INotification
	{
		public Operator User { get; }
		public Guid RoleID { get; }
		public string NewDescription { get; }

		public ChangeRoleDescriptionCommand(Operator user, Guid id, string newDescription)
		{
			User = user;
			RoleID = id;
			NewDescription = newDescription;
		}
	}
}
