﻿using System;
using System.Collections.Generic;
using System.Linq;
using Ledger;
using Ledger.Infrastructure;
using Magistrate.Domain.Events;

namespace Magistrate.Domain
{
	public class MagistrateSystem : AggregateRoot<Guid>
	{

		public IEnumerable<Permission> Permissions => _permissions;
		public IEnumerable<Role> Roles => _roles;
		public IEnumerable<User> Users => _users; 

		private readonly HashSet<Permission> _permissions;
		private readonly HashSet<Role> _roles;
		private readonly HashSet<User> _users;

		private readonly AggregateStore<Guid> _store;

		public MagistrateSystem(AggregateStore<Guid> store)
		{
			_store = store;
			_permissions = new HashSet<Permission>();
			_roles = new HashSet<Role>();
			_users = new HashSet<User>();
		}

		public void AddPermission(Permission permission)
		{
			_store.Save(permission);
			ApplyEvent(new PermissionAddedEvent { PermissionID = permission.ID });
		}

		public void RemovePermission(Permission permission)
		{
			ApplyEvent(new PermissionRemovedEvent { PermissionID = permission.ID });
		}

		public void AddRole(Role role)
		{
			_store.Save(role);
			ApplyEvent(new RoleAddedEvent { RoleID = role.ID });
		}

		public void RemoveRole(Role role)
		{
			ApplyEvent(new RoleRemovedEvent { RoleID = role.ID });
		}

		public void AddUser(User user)
		{
			_store.Save(user);
			ApplyEvent(new UserAddedEvent { UserID = user.ID });
		}

		public void RemoveUser(User user)
		{
			ApplyEvent(new UserRemovedEvent { UserID = user.ID });
		}




		private void Handle(PermissionAddedEvent e)
		{
			_permissions.Add(_store.Load(e.PermissionID, Permission.Blank));
		}

		private void Handle(PermissionRemovedEvent e)
		{
			var permission = _permissions.Single(p => p.ID == e.PermissionID);
			_permissions.Remove(permission);

			_roles.ForEach(role => role.RemovePermission(e.User, permission));
		}

		private void Handle(RoleAddedEvent e)
		{
			_roles.Add(_store.Load(e.RoleID, Role.Blank));
		}

		private void Handle(RoleRemovedEvent e)
		{
			_roles.RemoveWhere(r => r.ID == e.RoleID);
		}

		private void Handle(UserAddedEvent e)
		{
			_users.Add(_store.Load(e.UserID, User.Blank));
		}

		private void Handle(UserRemovedEvent e)
		{
			_users.RemoveWhere(u => u.ID == e.UserID);
		}
	}
}
