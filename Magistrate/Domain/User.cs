using System;
using System.Collections.Generic;
using System.Linq;
using Ledger;
using Magistrate.Domain.Events;

namespace Magistrate.Domain
{
	public class User : AggregateRoot<Guid>, IKeyed
	{
		public string Key { get; private set; }
		public string Name { get; private set; }
		public bool IsActive { get; private set; }

		public PermissionInspector Permissions => new PermissionInspector(_roles,_includes, _revokes);
		public IEnumerable<Role> Roles => _roles;

		private readonly HashSet<Permission> _includes;
		private readonly HashSet<Permission> _revokes;
		private readonly HashSet<Role> _roles;

		private readonly Func<Guid, Permission> _getPermission;
		private readonly Func<Guid, Role> _getRole;

		public User(Func<Guid, Permission> getPermission, Func<Guid, Role> getRole)
		{
			_getPermission = getPermission;
			_getRole = getRole;

			_includes = new HashSet<Permission>();
			_revokes = new HashSet<Permission>();
			_roles = new HashSet<Role>();

			IsActive = true;
		}

		public static User Create(Func<Guid, Permission> getPermission, Func<Guid, Role> getRole, string key, string name)
		{
			ValidateKey(key);
			ValidateName(name);

			var user = new User(getPermission, getRole);
			user.ApplyEvent(new UserCreatedEvent
			{
				ID = Guid.NewGuid(),
				Key = key,
				Name = name
			});

			return user;
		}

		public void ChangeName(string name)
		{
			ValidateName(name);
			ApplyEvent(new NameChangedEvent { NewName = name });
		}

		public void AddPermission(Permission permission)
		{
			ApplyEvent(new PermissionAddedEvent { PermissionID = permission.ID });
		}

		public void RemovePermission(Permission permission)
		{
			ApplyEvent(new PermissionRemovedEvent { PermissionID = permission.ID });
		}

		public void AddRole(Role role)
		{
			ApplyEvent(new RoleAddedEvent { RoleID = role.ID });
		}

		public void RemoveRole(Role role)
		{
			ApplyEvent(new RoleRemovedEvent { RoleID = role.ID });
		}

		public void Deactivate()
		{
			ApplyEvent(new UserDeactivatedEvent());
		}



		private static void ValidateKey(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentException("Key cannot be null or whitespace", nameof(key));
		}

		private static void ValidateName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Name cannot be null or whitespace", nameof(name));
		}



		private void Handle(UserCreatedEvent e)
		{
			ID = e.ID;
			Key = e.Key;
			Name = e.Name;
		}

		private void Handle(NameChangedEvent e)
		{
			Name = e.NewName;
		}

		private void Handle(PermissionAddedEvent e)
		{
			_revokes.RemoveWhere(r => r.ID == e.PermissionID);

			if (_roles.Any(r => r.Permissions.Any(p => p.ID == e.PermissionID)) == false)
				_includes.Add(_getPermission(e.PermissionID));
		}

		private void Handle(PermissionRemovedEvent e)
		{
			var removed = _includes.RemoveWhere(i => i.ID == e.PermissionID);

			if (removed == 0 || _roles.Any(r => r.Permissions.Any(p => p.ID == e.PermissionID)))
				_revokes.Add(_getPermission(e.PermissionID));
		}

		private void Handle(RoleAddedEvent e)
		{
			_roles.Add(_getRole(e.RoleID));
		}

		private void Handle(RoleRemovedEvent e)
		{
			_roles.RemoveWhere(r => r.ID == e.RoleID);
		}

		private void Handle(UserDeactivatedEvent e)
		{
			IsActive = false;
		}

	}
}
