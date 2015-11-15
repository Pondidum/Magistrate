using System;
using System.Collections.Generic;
using System.Linq;
using Ledger;
using Ledger.Infrastructure;
using Magistrate.Domain.Events;
using Magistrate.Domain.Rules;

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

		public void Save()
		{
			foreach (var permission in _permissions)
				_store.Save(permission);

			foreach (var role in _roles)
				_store.Save(role);

			foreach (var user in _users)
				_store.Save(user);

			_store.Save(this);
		}

		private void CheckRules<T>(IEnumerable<T> collection, T target) where T : AggregateRoot<Guid>, IKeyed
		{
			var rules = new[] { new UniqueKeyRule<T>(collection) };

			var violations = rules
				.Where(r => r.IsSatisfiedBy(target) == false)
				.Select(r => r.GetMessage(target))
				.ToList();

			if (violations.Any())
				throw new RuleViolationException(target, violations);
		}

		public void AddPermission(MagistrateUser currentUser, Permission permission)
		{
			CheckRules(_permissions, permission);

			_store.Save(permission);
			ApplyEvent(new PermissionAddedEvent { User = currentUser, PermissionID = permission.ID });
		}

		public void RemovePermission(MagistrateUser currentUser, Permission permission)
		{
			ApplyEvent(new PermissionRemovedEvent { User = currentUser, PermissionID = permission.ID });
		}

		public void AddRole(MagistrateUser currentUser, Role role)
		{
			CheckRules(_roles, role);

			_store.Save(role);
			ApplyEvent(new RoleAddedEvent { User = currentUser, RoleID = role.ID });
		}

		public void RemoveRole(MagistrateUser currentUser, Role role)
		{
			ApplyEvent(new RoleRemovedEvent { User = currentUser, RoleID = role.ID });
		}

		public void AddUser(MagistrateUser currentUser, User user)
		{
			CheckRules(_users, user);

			_store.Save(user);
			ApplyEvent(new UserAddedEvent { User = currentUser, UserID = user.ID });
		}

		public void RemoveUser(MagistrateUser currentUser, User user)
		{
			ApplyEvent(new UserRemovedEvent { User = currentUser, UserID = user.ID });
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
			//_users.ForEach(user => user.RemovePermission(e.User, permission));
		}

		private void Handle(RoleAddedEvent e)
		{
			_roles.Add(_store.Load(e.RoleID, Role.Blank));
		}

		private void Handle(RoleRemovedEvent e)
		{
			var role = _roles.Single(r => r.ID == e.RoleID);
			_roles.Remove(role);

			_users.ForEach(user => user.RemoveRole(e.User, role));
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
