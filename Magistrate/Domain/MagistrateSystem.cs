using System;
using System.Collections.Generic;
using Ledger;
using Magistrate.Domain.Events.SystemEvents;

namespace Magistrate.Domain
{
	public class MagistrateSystem : AggregateRoot<Guid>
	{
		private readonly HashSet<Guid> _permissions;
		private readonly HashSet<Guid> _roles;
		private readonly HashSet<Guid> _users;

		public MagistrateSystem()
		{
			_permissions = new HashSet<Guid>();
			_roles = new HashSet<Guid>();
			_users = new HashSet<Guid>();
		}

		public void AddPermission(MagistrateUser currentUser, Permission permission)
		{
			ApplyEvent(new PermissionAddedToSystemEvent { User = currentUser, PermissionID = permission.ID });
		}

		public void AddRole(MagistrateUser currentUser, Role role)
		{
			ApplyEvent(new RoleAddedToSystemEvent { User = currentUser, RoleID = role.ID });
		}

		public void AddUser(MagistrateUser currentUser, User user)
		{
			ApplyEvent(new UserAddedToSystemEvent { User = currentUser, UserID = user.ID });
		}

		private void Handle(PermissionAddedToSystemEvent e)
		{
			_permissions.Add(e.PermissionID);
		}

		private void Handle(RoleAddedToSystemEvent e)
		{
			_roles.Add(e.RoleID);
		}

		private void Handle(UserAddedToSystemEvent e)
		{
			_users.Add(e.UserID);
		}
	}
}
