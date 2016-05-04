using System;
using Ledger;
using Magistrate.Domain.Commands;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class ChangeUserNameCommandHandler : INotificationHandler<ChangeUserNameCommand>
	{
		private readonly AggregateStore<Guid> _store;

		public ChangeUserNameCommandHandler(AggregateStore<Guid> store)
		{
			_store = store;
		}

		public void Handle(ChangeUserNameCommand notification)
		{
			var user = _store.Load(MagistrateSystem.MagistrateStream, notification.UserID, User.Blank);

			user.ChangeName(notification.Operator, notification.NewName);

			_store.Save(MagistrateSystem.MagistrateStream, user);
		}
	}
}
