using System;
using Ledger;
using Magistrate.Domain.Commands;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class RemoveRevokesFromUserCommandHandler : INotificationHandler<RemoveRevokesFromUserCommand>
	{
		private readonly AggregateStore<Guid> _store;

		public RemoveRevokesFromUserCommandHandler(AggregateStore<Guid> store)
		{
			_store = store;
		}

		public void Handle(RemoveRevokesFromUserCommand notification)
		{
			var user = _store.Load(MagistrateSystem.MagistrateStream, notification.UserID, User.Blank);

			foreach (var id in notification.PermissionIDs)
				user.RemoveRevoke(notification.Operator, id);

			_store.Save(MagistrateSystem.MagistrateStream, user);
		}
	}
}
