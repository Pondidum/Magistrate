using System;
using MediatR;

namespace Magistrate.Domain.Commands
{
	public class ChangeRoleDescriptionCommand : INotification
	{
		public Operator Operator { get; }
		public Guid RoleID { get; }
		public string NewDescription { get; }

		public ChangeRoleDescriptionCommand(Operator @operator, Guid id, string newDescription)
		{
			Operator = @operator;
			RoleID = id;
			NewDescription = newDescription;
		}
	}
}
