using System;
using Ledger;
using Magistrate.Domain.Commands;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class DeleteUserCommandHandler : INotificationHandler<DeleteUserCommand>
	{
		private readonly AggregateStore<Guid> _store;

		public DeleteUserCommandHandler(AggregateStore<Guid> store)
		{
			_store = store;
		}

		public void Handle(DeleteUserCommand notification)
		{
			var user = _store.Load(MagistrateSystem.MagistrateStream, notification.UserID, User.Blank);

			user.Delete(notification.Operator);

			_store.Save(MagistrateSystem.MagistrateStream, user);
		}
	}
}