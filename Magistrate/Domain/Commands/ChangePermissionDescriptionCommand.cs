using System;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class ChangePermissionDescriptionCommand : INotification
	{
		public Operator Operator { get; }
		public Guid PermissionID { get; }
		public string NewDescription { get; }

		public ChangePermissionDescriptionCommand(Operator @operator, Guid id, string newDescription)
		{
			Operator = @operator;
			PermissionID = id;
			NewDescription = newDescription;
		}
	}
}
