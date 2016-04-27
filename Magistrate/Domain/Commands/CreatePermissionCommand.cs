using MediatR;

namespace Magistrate.Domain.Commands
{
	public class CreatePermissionCommand : INotification
	{
		public PermissionKey Key { get; }
		public string Name { get; }
		public string Description { get; }
		public MagistrateUser Operator { get; set; }

		public CreatePermissionCommand(PermissionKey key, string name, string description)
		{
			Key = key;
			Name = name;
			Description = description;
		}
	}
}
