using System;
using System.Collections.Generic;
using Ledger;
using Ledger.Infrastructure;
using Magistrate.Domain.Events;

namespace Magistrate.Domain
{
	public class User : AggregateRoot<Guid>, IKeyed
	{
		public string Key { get; private set; }
		public string Name { get; private set; }

		public IEnumerable<Guid> Includes => _includes;
		public IEnumerable<Guid> Revokes => _revokes;
		public IEnumerable<Guid> Roles => _roles;


		private readonly HashSet<Guid> _includes;
		private readonly HashSet<Guid> _revokes;
		private readonly HashSet<Guid> _roles;

		private User()
		{
			_includes = new HashSet<Guid>();
			_revokes = new HashSet<Guid>();
			_roles = new HashSet<Guid>();
		}

		public static User Blank()
		{
			return new User();
		}

		public static User Create(MagistrateUser currentUser, string key, string name)
		{
			ValidateKey(key);
			ValidateName(name);

			var user = new User();
			user.ApplyEvent(new UserCreatedEvent
			{
				ID = Guid.NewGuid(),
				User = currentUser,
				Key = key,
				Name = name
			});

			return user;
		}

		public void ChangeName(MagistrateUser currentUser, string name)
		{
			ValidateName(name);
			ApplyEvent(new UserNameChangedEvent
			{
				User = currentUser,
				NewName = name
			});
		}

		public void AddInclude(MagistrateUser currentUser, Permission permission)
		{
			ApplyEvent(new IncludeAddedEvent
			{
				User = currentUser,
				PermissionID = permission.ID
			});
		}

		public void RemoveInclude(MagistrateUser currentUser, Permission permission)
		{
			ApplyEvent(new IncludeRemovedEvent
			{
				User = currentUser,
				PermissionID = permission.ID
			});
		}

		public void AddRevoke(MagistrateUser currentUser, Permission permission)
		{
			ApplyEvent(new RevokeAddedEvent
			{
				User = currentUser,
				PermissionID = permission.ID
			});
		}

		public void RemoveRevoke(MagistrateUser currentUser, Permission permission)
		{
			ApplyEvent(new RevokeRemovedEvent
			{
				User = currentUser,
				PermissionID = permission.ID
			});
		}

		public void AddRole(MagistrateUser currentUser, Role role)
		{
			ApplyEvent(new RoleAddedEvent
			{
				User = currentUser,
				RoleID = role.ID
			});
		}

		public void RemoveRole(MagistrateUser currentUser, Role role)
		{
			ApplyEvent(new RoleRemovedEvent
			{
				User = currentUser,
				RoleID = role.ID
			});
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

		private void Handle(UserNameChangedEvent e)
		{
			Name = e.NewName;
		}

		private void Handle(RoleAddedEvent e)
		{
			_roles.Add(e.RoleID);
		}

		private void Handle(RoleRemovedEvent e)
		{
			_roles.Remove(e.RoleID);
		}

		private void Handle(IncludeAddedEvent e)
		{
			_revokes.Remove(e.PermissionID);
			_includes.Add(e.PermissionID);
		}

		private void Handle(IncludeRemovedEvent e)
		{
			_includes.Remove(e.PermissionID);
		}

		private void Handle(RevokeAddedEvent e)
		{
			if (_includes.Contains(e.PermissionID))
				_includes.Remove(e.PermissionID);
			else
				_revokes.Add(e.PermissionID);
		}

		private void Handle(RevokeRemovedEvent e)
		{
			_revokes.Remove(e.PermissionID);
		}
	}
}
