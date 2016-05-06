using System;
using Ledger;
using Magistrate.Domain.Commands;
using MediatR;

namespace Magistrate.Domain.CommandHandlers
{
	public class ChangeRoleDescriptionCommandHandler : INotificationHandler<ChangeRoleDescriptionCommand>
	{
		private readonly AggregateStore<Guid> _store;

		public ChangeRoleDescriptionCommandHandler(AggregateStore<Guid> store)
		{
			_store = store;
		}

		public void Handle(ChangeRoleDescriptionCommand notification)
		{
			var role = _store.Load(MagistrateSystem.MagistrateStream, notification.RoleID, Role.Blank);

			role.ChangeDescription(notification.Operator, notification.NewDescription);

			_store.Save(MagistrateSystem.MagistrateStream, role);
		}
	}
}
