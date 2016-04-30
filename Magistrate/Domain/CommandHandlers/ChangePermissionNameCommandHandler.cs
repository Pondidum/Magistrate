using System;
using Ledger;
using Magistrate.Domain.Commands;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class ChangePermissionNameCommandHandler : INotificationHandler<ChangePermissionNameCommand>
	{
		private readonly AggregateStore<Guid> _store;

		public ChangePermissionNameCommandHandler(AggregateStore<Guid> store)
		{
			_store = store;
		}

		public void Handle(ChangePermissionNameCommand notification)
		{
			var permission = _store.Load(MagistrateSystem.MagistrateStream, notification.PermissionID, Permission.Blank);

			permission.ChangeName(notification.User, notification.NewName);

			_store.Save(MagistrateSystem.MagistrateStream, permission);
		}
	}
}