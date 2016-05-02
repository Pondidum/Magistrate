using System;
using Ledger;
using Magistrate.Domain.Commands;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class AddPermissionsToRoleCommandHandler : INotificationHandler<AddPermissionsToRoleCommand>
	{
		private readonly AggregateStore<Guid> _store;

		public AddPermissionsToRoleCommandHandler(AggregateStore<Guid> store)
		{
			_store = store;
		}

		public void Handle(AddPermissionsToRoleCommand notification)
		{
			var role = _store.Load(MagistrateSystem.MagistrateStream, notification.RoleID, Role.Blank);

			foreach (var permissionID in notification.PermissionIDs)
				role.AddPermission(notification.Operator, permissionID);

			_store.Save(MagistrateSystem.MagistrateStream, role);
		}
	}
}
