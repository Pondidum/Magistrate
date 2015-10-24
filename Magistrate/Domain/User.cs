using System;
using System.Collections.Generic;
using Ledger;
using Magistrate.Domain.Events;

namespace Magistrate.Domain
{
	public class User : AggregateRoot<Guid>
	{
		public string Key { get; private set; }
		public string Name { get; private set; }

		public IEnumerable<Permission> Includes => _includes;
		public IEnumerable<Permission> Revokes => _revokes;

		private readonly HashSet<Permission> _includes;
		private readonly HashSet<Permission> _revokes;

		private readonly Func<Guid, Permission> _getPermission;

		public User(Func<Guid, Permission> getPermission)
		{
			_getPermission = getPermission;
			_includes = new HashSet<Permission>();
			_revokes = new HashSet<Permission>();
		}

		public static User Create(Func<Guid, Permission> getPermission, string key, string name)
		{
			ValidateKey(key);
			ValidateName(name);

			var user = new User(getPermission);
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

		public void AddPermission(Permission permission)
		{
			ApplyEvent(new PermissionAddedEvent { PermissionID = permission.ID });
		}

		public void RemovePermission(Permission permission)
		{
			ApplyEvent(new PermissionRemovedEvent { PermissionID = permission.ID });
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

			_includes.Add(_getPermission(e.PermissionID));
		}

		private void Handle(PermissionRemovedEvent e)
		{
			var removed = _includes.RemoveWhere(i => i.ID == e.PermissionID);

			if (removed == 0)
				_revokes.Add(_getPermission(e.PermissionID));
		}
	}
}
