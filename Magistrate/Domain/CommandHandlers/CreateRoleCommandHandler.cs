using System;
using Ledger;
using Magistrate.Domain.Commands;
using Magistrate.Domain.Services;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class CreateRoleCommandHandler : INotificationHandler<CreateRoleCommand>
	{
		private readonly AggregateStore<Guid> _store;
		private readonly RoleService _service;

		public CreateRoleCommandHandler(AggregateStore<Guid> store, RoleService service)
		{
			_store = store;
			_service = service;
		}

		public void Handle(CreateRoleCommand notification)
		{
			var role = Role.Create(
				_service,
				notification.Operator,
				notification.Key,
				notification.Name,
				notification.Description);

			_store.Save("Magistrate", role);
		}
	}
}
