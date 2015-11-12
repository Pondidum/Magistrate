using System;
using System.Collections.Generic;
using Ledger;
using Magistrate.Domain.Events;

namespace Magistrate.Domain
{
	public class MagistrateSystem : AggregateRoot<Guid>
	{

		public IEnumerable<Permission> Permissions => _permissions;
		 
		private readonly HashSet<Permission> _permissions;

		private readonly AggregateStore<Guid> _store;

		public MagistrateSystem(AggregateStore<Guid> store)
		{
			_store = store;
			_permissions = new HashSet<Permission>();
		}

		public void AddPermission(Permission permission)
		{
			_store.Save(permission);

			ApplyEvent(new PermissionAddedEvent
			{
				PermissionID = permission.ID
			});
		}

		public void RemovePermission(Permission permission)
		{
			ApplyEvent(new PermissionRemovedEvent
			{
				PermissionID = permission.ID
			});
		}




		private void Handle(PermissionAddedEvent e)
		{
			_permissions.Add(_store.Load(e.PermissionID, Permission.Blank));
		}

		private void Handle(PermissionRemovedEvent e)
		{
			_permissions.RemoveWhere(p => p.ID == e.PermissionID);
		}

	}
}
