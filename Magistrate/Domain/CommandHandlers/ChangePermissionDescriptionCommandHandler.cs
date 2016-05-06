using System;
using Ledger;
using Magistrate.Domain.Commands;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class ChangePermissionDescriptionCommandHandler : INotificationHandler<ChangePermissionDescriptionCommand>
	{
		private readonly AggregateStore<Guid> _store;

		public ChangePermissionDescriptionCommandHandler(AggregateStore<Guid> store)
		{
			_store = store;
		}

		public void Handle(ChangePermissionDescriptionCommand notification)
		{
			var permission = _store.Load(MagistrateSystem.MagistrateStream, notification.PermissionID, Permission.Blank);

			permission.ChangeDescription(notification.Operator, notification.NewDescription);

			_store.Save(MagistrateSystem.MagistrateStream, permission);
		}
	}
}
