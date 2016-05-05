using System;
using Ledger;
using Magistrate.Domain.Commands;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class AddRolesToUserCommandHandler : INotificationHandler<AddRolesToUserCommand>
	{
		private readonly AggregateStore<Guid> _store;

		public AddRolesToUserCommandHandler(AggregateStore<Guid> store)
		{
			_store = store;
		}

		public void Handle(AddRolesToUserCommand notification)
		{
			var user = _store.Load(MagistrateSystem.MagistrateStream, notification.UserID, User.Blank);

			foreach (var role in notification.RoleIDs)
				user.AddRole(notification.Operator, role);

			_store.Save(MagistrateSystem.MagistrateStream, user);
		}
	}
}
