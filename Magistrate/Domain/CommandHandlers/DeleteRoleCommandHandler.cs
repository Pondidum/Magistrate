using System;
using Ledger;
using Magistrate.Domain.Commands;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class DeleteRoleCommandHandler : INotificationHandler<DeleteRoleCommand>
	{
		private readonly AggregateStore<Guid> _store;

		public DeleteRoleCommandHandler(AggregateStore<Guid> store)
		{
			_store = store;
		}

		public void Handle(DeleteRoleCommand notification)
		{
			var role = _store.Load(MagistrateSystem.MagistrateStream, notification.RoleID, Role.Blank);

			role.Delete(notification.Operator);

			_store.Save(MagistrateSystem.MagistrateStream, role);
		}
	}
}
