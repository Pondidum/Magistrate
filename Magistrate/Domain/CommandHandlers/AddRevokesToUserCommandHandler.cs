using System;
using Ledger;
using Magistrate.Domain.Commands;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class AddRevokesToUserCommandHandler : INotificationHandler<AddRevokesToUserCommand>
	{
		private readonly AggregateStore<Guid> _store;

		public AddRevokesToUserCommandHandler(AggregateStore<Guid> store)
		{
			_store = store;
		}

		public void Handle(AddRevokesToUserCommand notification)
		{
			var user = _store.Load(MagistrateSystem.MagistrateStream, notification.UserID, User.Blank);

			foreach (var id in notification.PermissionIDs)
				user.AddRevoke(notification.Operator, id);

			_store.Save(MagistrateSystem.MagistrateStream, user);
		}
	}
}
