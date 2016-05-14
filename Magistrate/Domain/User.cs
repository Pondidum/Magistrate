using System;
using System.Collections.Generic;
using System.Security;
using Ledger;
using Magistrate.Domain.Events.UserEvents;
using Magistrate.Domain.Services;

namespace Magistrate.Domain
{
	public class User : AggregateRoot<Guid>, IIdentity
	{
		public UserKey Key { get; private set; }
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

		public static User Create(UserService service, Operator currentUser, UserKey key, string name)
		{
			if (currentUser.CanCreateUsers == false) throw new SecurityException($"{currentUser.Name} cannot create users.");

			if (service.CanCreateUser(key) == false)
				throw new ArgumentException($"There is already a User with the Key '{key}'", nameof(key));

			ValidateName(name);

			var user = new User();
			user.ApplyEvent(new UserCreatedEvent
			(
				currentUser,
				Guid.NewGuid(),
				key,
				name
			));

			return user;
		}

		public void ChangeName(Operator currentUser, string newName)
		{
			ValidateName(newName);
			ApplyEvent(new UserNameChangedEvent
			(
				currentUser,
				newName
			));
		}

		public void Delete(Operator currentUser)
		{
			ApplyEvent(new UserDeletedEvent
			(
				currentUser
			));
		}

		public void AddInclude(Operator currentUser, Guid permissionID)
		{
			if (_includes.Contains(permissionID))
				return;

			if (_revokes.Contains(permissionID))
				ApplyEvent(new RevokeRemovedFromUserEvent(currentUser, permissionID));

			ApplyEvent(new IncludeAddedToUserEvent(currentUser, permissionID));
		}

		public void RemoveInclude(Operator currentUser, Guid permissionID)
		{
			if (_includes.Contains(permissionID) == false)
				return;

			ApplyEvent(new IncludeRemovedFromUserEvent
			(
				currentUser,
				permissionID
			));
		}

		public void AddRevoke(Operator currentUser, Guid permissionID)
		{
			if (_revokes.Contains(permissionID))
				return;

			if (_includes.Contains(permissionID))
				ApplyEvent(new IncludeRemovedFromUserEvent(currentUser, permissionID));

			ApplyEvent(new RevokeAddedToUserEvent(currentUser, permissionID));
		}

		public void RemoveRevoke(Operator currentUser, Guid permissionID)
		{
			if (_revokes.Contains(permissionID) == false)
				return;

			ApplyEvent(new RevokeRemovedFromUserEvent
			(
				currentUser,
				permissionID
			));
		}

		public void AddRole(Operator currentUser, Guid roleID)
		{
			if (_roles.Contains(roleID))
				return;

			ApplyEvent(new RoleAddedToUserEvent
			(
				currentUser,
				roleID
			));
		}

		public void RemoveRole(Operator currentUser, Guid roleID)
		{
			if (_roles.Contains(roleID) == false)
				return;

			ApplyEvent(new RoleRemovedFromUserEvent
			(
				currentUser,
				roleID
			));
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

		private void Handle(UserDeletedEvent e)
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
			_includes.Add(e.PermissionID);
		}

		private void Handle(IncludeRemovedFromUserEvent e)
		{
			_includes.Remove(e.PermissionID);
		}

		private void Handle(RevokeAddedToUserEvent e)
		{
			_revokes.Add(e.PermissionID);
		}

		private void Handle(RevokeRemovedFromUserEvent e)
		{
			_revokes.Remove(e.PermissionID);
		}
	}
}
