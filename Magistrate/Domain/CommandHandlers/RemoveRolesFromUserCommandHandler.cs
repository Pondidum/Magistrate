using System;
using Ledger;
using Magistrate.Domain.Commands;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class RemoveRolesFromUserCommandHandler : INotificationHandler<RemoveRolesFromUserCommand>
	{
		private readonly AggregateStore<Guid> _store;

		public RemoveRolesFromUserCommandHandler(AggregateStore<Guid> store)
		{
			_store = store;
		}

		public void Handle(RemoveRolesFromUserCommand notification)
		{
			var user = _store.Load(MagistrateSystem.MagistrateStream, notification.UserID, User.Blank);

			foreach (var role in notification.RoleIDs)
				user.RemoveRole(notification.Operator, role);

			_store.Save(MagistrateSystem.MagistrateStream, user);
		}
	}
}
