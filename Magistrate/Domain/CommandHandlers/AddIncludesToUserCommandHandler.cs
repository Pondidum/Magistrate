using System;
using Ledger;
using Magistrate.Domain.Commands;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class AddIncludesToUserCommandHandler : INotificationHandler<AddIncludesToUserCommand>
	{
		private readonly AggregateStore<Guid> _store;

		public AddIncludesToUserCommandHandler(AggregateStore<Guid> store)
		{
			_store = store;
		}

		public void Handle(AddIncludesToUserCommand notification)
		{
			var user = _store.Load(MagistrateSystem.MagistrateStream, notification.UserID, User.Blank);

			foreach (var id in notification.PermissionIDs)
				user.AddInclude(notification.Operator, id);

			_store.Save(MagistrateSystem.MagistrateStream, user);
		}
	}
}
