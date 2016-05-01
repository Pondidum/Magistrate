using System;
using Ledger;
using Magistrate.Domain.Commands;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class ChangeRoleNameCommandHandler : INotificationHandler<ChangeRoleNameCommand>
	{
		private readonly AggregateStore<Guid> _store;

		public ChangeRoleNameCommandHandler(AggregateStore<Guid> store)
		{
			_store = store;
		}

		public void Handle(ChangeRoleNameCommand notification)
		{
			var role = _store.Load(MagistrateSystem.MagistrateStream, notification.RoleID, Role.Blank);

			role.ChangeName(notification.User, notification.NewName);

			_store.Save(MagistrateSystem.MagistrateStream, role);
		}
	}
}
