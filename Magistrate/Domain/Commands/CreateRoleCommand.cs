using MediatR;

namespace Magistrate.Domain.Commands
{
	public class CreateRoleCommand : INotification
	{
		public Operator Operator { get; set; }
		public RoleKey Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public CreateRoleCommand(Operator user, RoleKey key, string name, string description)
		{
			Operator = user;
			Key = key;
			Name = name;
			Description = description;
		}
	}
}
