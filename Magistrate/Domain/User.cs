using System;
using System.Collections.Generic;
using Ledger;
using Magistrate.Domain.Events.UserEvents;

namespace Magistrate.Domain
{
	public class User : AggregateRoot<Guid>, IKeyed, IIdentity
	{
		public string Key { get; private set; }
		public string Name { get; private set; }
		public bool IsActive { get; private set; }

		public IEnumerable<Guid> Includes => _includes;
		public IEnumerable<Guid> Revokes => _revokes;
		public IEnumerable<Guid> Roles => _roles;

		private readonly HashSet<Guid> _includes;
		private readonly HashSet<Guid> _revokes;
		private readonly HashSet<Guid> _roles;

		private User()
		{
			IsActive = true;
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

		public void Deactivate(MagistrateUser currentUser)
		{
			ApplyEvent(new UserDeactivatedEvent()
			{
				User = currentUser
			});
		}

		public void AddInclude(MagistrateUser currentUser, Permission permission)
		{
			ApplyEvent(new IncludeAddedToUserEvent
			{
				User = currentUser,
				PermissionID = permission.ID
			});
		}

		public void RemoveInclude(MagistrateUser currentUser, Permission permission)
		{
			ApplyEvent(new IncludeRemovedFromUserEvent
			{
				User = currentUser,
				PermissionID = permission.ID
			});
		}

		public void AddRevoke(MagistrateUser currentUser, Permission permission)
		{
			ApplyEvent(new RevokeAddedToUserEvent
			{
				User = currentUser,
				PermissionID = permission.ID
			});
		}

		public void RemoveRevoke(MagistrateUser currentUser, Permission permission)
		{
			ApplyEvent(new RevokeRemovedFromUserEvent
			{
				User = currentUser,
				PermissionID = permission.ID
			});
		}

		public void AddRole(MagistrateUser currentUser, Role role)
		{
			ApplyEvent(new RoleAddedToUserEvent
			{
				User = currentUser,
				RoleID = role.ID
			});
		}

		public void RemoveRole(MagistrateUser currentUser, Role role)
		{
			ApplyEvent(new RoleRemovedFromUserEvent
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

		private void Handle(UserDeactivatedEvent e)
		{
			IsActive = false;
		}

		private void Handle(RoleAddedToUserEvent e)
		{
			_roles.Add(e.RoleID);
		}

		private void Handle(RoleRemovedFromUserEvent e)
		{
			_roles.Remove(e.RoleID);
		}

		private void Handle(IncludeAddedToUserEvent e)
		{
			_revokes.Remove(e.PermissionID);
			_includes.Add(e.PermissionID);
		}

		private void Handle(IncludeRemovedFromUserEvent e)
		{
			_includes.Remove(e.PermissionID);
		}

		private void Handle(RevokeAddedToUserEvent e)
		{
			if (_includes.Contains(e.PermissionID))
				_includes.Remove(e.PermissionID);
			else
				_revokes.Add(e.PermissionID);
		}

		private void Handle(RevokeRemovedFromUserEvent e)
		{
			_revokes.Remove(e.PermissionID);
		}
	}
}
