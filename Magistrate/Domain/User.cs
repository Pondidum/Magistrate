using System;
using System.Collections.Generic;
using System.Security;
using Ledger;
using Magistrate.Domain.Events.UserEvents;
using Magistrate.Domain.Services;

namespace Magistrate.Domain
{
	public class User : AggregateRoot<Guid>, IKeyed<UserKey>, IIdentity
	{
		public UserKey Key { get; private set; }
		public string Name { get; private set; }
		public bool IsActive { get; private set; }

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

		public static User Create(UserService service, MagistrateUser currentUser, UserKey key, string name)
		{
			if (currentUser.CanCreateUsers == false) throw new SecurityException($"{currentUser.Name} cannot create users.");

			service.AssertCanCreateUser(key);

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

		public void ChangeName(MagistrateUser currentUser, string newName)
		{
			ValidateName(newName);
			ApplyEvent(new UserNameChangedEvent
			(
				currentUser,
				Name,
				newName
			));
		}

		public void Deactivate(MagistrateUser currentUser)
		{
			ApplyEvent(new UserDeactivatedEvent
			(
				currentUser,
				Name
			));
		}

		public void AddInclude(MagistrateUser currentUser, Permission permission)
		{
			ApplyEvent(new IncludeAddedToUserEvent
			(
				currentUser,
				permission.ID,
				permission.Name,
				Name
			));
		}

		public void RemoveInclude(MagistrateUser currentUser, Permission permission)
		{
			ApplyEvent(new IncludeRemovedFromUserEvent
			(
				currentUser,
				permission.ID,
				permission.Name,
				Name
			));
		}

		public void AddRevoke(MagistrateUser currentUser, Permission permission)
		{
			ApplyEvent(new RevokeAddedToUserEvent
			(
				currentUser,
				permission.ID,
				permission.Name,
				Name
			));
		}

		public void RemoveRevoke(MagistrateUser currentUser, Permission permission)
		{
			ApplyEvent(new RevokeRemovedFromUserEvent
			(
				currentUser,
				permission.ID,
				permission.Name,
				Name
			));
		}

		public void AddRole(MagistrateUser currentUser, Role role)
		{
			ApplyEvent(new RoleAddedToUserEvent
			(
				currentUser,
				role.ID,
				role.Name,
				Name
			));
		}

		public void RemoveRole(MagistrateUser currentUser, Role role)
		{
			ApplyEvent(new RoleRemovedFromUserEvent
			(
				currentUser,
				role.ID,
				role.Name,
				Name
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
			_includes.Remove(e.PermissionID);
			_revokes.Add(e.PermissionID);
		}

		private void Handle(RevokeRemovedFromUserEvent e)
		{
			_revokes.Remove(e.PermissionID);
		}
	}
}
