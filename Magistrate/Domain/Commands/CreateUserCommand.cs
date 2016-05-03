using MediatR;

namespace Magistrate.Domain.Commands
{
	public class CreateUserCommand : INotification
	{
		public Operator Operator { get; }
		public UserKey Key { get; }
		public string Name { get; }

		public CreateUserCommand(Operator user, UserKey key, string name)
		{
			Operator = user;
			Key = key;
			Name = name;
		}
	}
}
