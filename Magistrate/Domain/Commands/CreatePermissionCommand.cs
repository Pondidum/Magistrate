using MediatR;

namespace Magistrate.Domain.Commands
{
	public class CreatePermissionCommand : INotification
	{
		public PermissionKey Key { get; }
		public string Name { get; }
		public string Description { get; }
		public Operator Operator { get; }

		public CreatePermissionCommand(Operator user, PermissionKey key, string name, string description)
		{
			Operator = user;
			Key = key;
			Name = name;
			Description = description;
		}
	}
}
