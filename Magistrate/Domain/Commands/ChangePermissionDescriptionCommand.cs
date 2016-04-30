using System;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class ChangePermissionDescriptionCommand : INotification
	{
		public Operator User { get; }
		public Guid PermissionID { get; }
		public string NewDescription { get; }

		public ChangePermissionDescriptionCommand(Operator user, Guid id, string newDescription)
		{
			User = user;
			PermissionID = id;
			NewDescription = newDescription;
		}
	}
}
