using System;
using Ledger;
using Magistrate.Domain.Commands;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class DeletePermissionCommandHandler : INotificationHandler<DeletePermissionCommand>
	{
		private readonly AggregateStore<Guid> _store;

		public DeletePermissionCommandHandler(AggregateStore<Guid> store)
		{
			_store = store;
		}

		public void Handle(DeletePermissionCommand notification)
		{
			var permission = _store.Load(MagistrateSystem.MagistrateStream, notification.PermissionID, Permission.Blank);

			permission.Delete(notification.Operator);

			_store.Save(MagistrateSystem.MagistrateStream, permission);
		}
	}
}
