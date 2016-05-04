using System;
using Ledger;
using Magistrate.Domain.Commands;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class RemoveIncludesFromUserCommandHandler : INotificationHandler<RemoveIncludesFromUserCommand>
	{
		private readonly AggregateStore<Guid> _store;

		public RemoveIncludesFromUserCommandHandler(AggregateStore<Guid> store)
		{
			_store = store;
		}

		public void Handle(RemoveIncludesFromUserCommand notification)
		{
			var user = _store.Load(MagistrateSystem.MagistrateStream, notification.UserID, User.Blank);

			foreach (var id in notification.PermissionIDs)
				user.RemoveInclude(notification.Operator, id);

			_store.Save(MagistrateSystem.MagistrateStream, user);
		}
	}
}
