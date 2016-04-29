using System;
using Ledger;
using Magistrate.Domain.Commands;
using Magistrate.Domain.Services;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class CreatePermissionCommandHandler : INotificationHandler<CreatePermissionCommand>
	{
		private readonly AggregateStore<Guid> _store;
		private readonly PermissionService _service;

		public CreatePermissionCommandHandler(AggregateStore<Guid> store, PermissionService service)
		{
			_store = store;
			_service = service;
		}

		public void Handle(CreatePermissionCommand notification)
		{
			var permission = Permission.Create(
				_service,
				notification.Operator,
				notification.Key,
				notification.Name,
				notification.Description);

			_store.Save("Magistrate", permission);
		}
	}
}
