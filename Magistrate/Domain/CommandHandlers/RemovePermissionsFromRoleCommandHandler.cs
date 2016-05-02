using System;
using Ledger;
using Magistrate.Domain.Commands;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class RemovePermissionsFromRoleCommandHandler : INotificationHandler<RemovePermissionsFromRoleCommand>
	{
		private readonly AggregateStore<Guid> _store;

		public RemovePermissionsFromRoleCommandHandler(AggregateStore<Guid> store)
		{
			_store = store;
		}

		public void Handle(RemovePermissionsFromRoleCommand notification)
		{
			var role = _store.Load(MagistrateSystem.MagistrateStream, notification.RoleID, Role.Blank);

			foreach (var permissionID in notification.PermissionIDs)
				role.AddPermission(notification.Operator, permissionID);

			_store.Save(MagistrateSystem.MagistrateStream, role);
		}
	}
}
