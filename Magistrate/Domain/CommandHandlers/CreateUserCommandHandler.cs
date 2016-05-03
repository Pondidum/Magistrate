using System;
using Ledger;
using Magistrate.Domain.Commands;
using Magistrate.Domain.Services;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class CreateUserCommandHandler : INotificationHandler<CreateUserCommand>
	{
		private readonly AggregateStore<Guid> _store;
		private readonly UserService _userService;

		public CreateUserCommandHandler(AggregateStore<Guid> store, UserService userService)
		{
			_store = store;
			_userService = userService;
		}

		public void Handle(CreateUserCommand notification)
		{
			var user = User.Create(
				_userService,
				notification.Operator,
				notification.Key,
				notification.Name);

			_store.Save(MagistrateSystem.MagistrateStream, user);
		}
	}
}
