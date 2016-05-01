using System;
using System.Collections.Generic;
using System.Security;
using Ledger;
using Magistrate.Domain.Events.RoleEvents;
using Magistrate.Domain.Services;

namespace Magistrate.Domain
{
	public class Role : AggregateRoot<Guid>, IIdentity
	{
		public RoleKey Key { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
		public bool IsActive { get; private set; }

		public IEnumerable<Guid> Permissions => _permissions;

		private readonly HashSet<Guid> _permissions;

		private Role()
		{
			IsActive = true;
			_permissions = new HashSet<Guid>();
		}

		public static Role Blank()
		{
			return new Role();
		}

		public static Role Create(RoleService service, Operator user, RoleKey key, string name, string description)
		{
			if (user.CanCreateRoles == false) throw new SecurityException($"{user.Name} cannot create roles.");

			if (service.CanCreateRole(key) == false)
				throw new ArgumentException($"There is already a Role with the Key '{key}'", nameof(key));

			ValidateName(name);

			var role = new Role();
			role.ApplyEvent(new RoleCreatedEvent
			(
				user,
				Guid.NewGuid(),
				key,
				name,
				description
			));

			return role;
		}

		public void ChangeName(Operator user, string newName)
		{
			ValidateName(newName);

			if (Name == newName)
				return;

			ApplyEvent(new RoleNameChangedEvent
			(
				user,
				Name,
				newName
			));
		}

		public void ChangeDescription(Operator user, string newDescription)
		{
			if (Description == newDescription)
				return;

			ApplyEvent(new RoleDescriptionChangedEvent
			(
				user,
				newDescription,
				Name
			));
		}

		public void Delete(Operator user)
		{
			ApplyEvent(new RoleDeletedEvent
			(
				user,
				Name
			));
		}

		public void AddPermission(Operator user, Permission permission)
		{
			ApplyEvent(new PermissionAddedToRoleEvent
			(
				user,
				permission.ID,
				permission.Name,
				Name
			));
		}

		public void RemovePermission(Operator user, Permission permission)
		{
			ApplyEvent(new PermissionRemovedFromRoleEvent
			(
				user,
				permission.ID,
				permission.Name,
				Name
			));
		}


		private static void ValidateName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Name cannot be null or whitespace", nameof(name));
		}



		private void Handle(RoleCreatedEvent e)
		{
			ID = e.ID;
			Key = e.Key;
			Name = e.Name;
			Description = e.Description;
		}

		private void Handle(RoleNameChangedEvent e)
		{
			Name = e.NewName;
		}

		private void Handle(RoleDeletedEvent e)
		{
			IsActive = false;
		}

		private void Handle(RoleDescriptionChangedEvent e)
		{
			Description = e.NewDescription;
		}

		private void Handle(PermissionAddedToRoleEvent e)
		{
			_permissions.Add(e.PermissionID);
		}

		private void Handle(PermissionRemovedFromRoleEvent e)
		{
			_permissions.Remove(e.PermissionID);
		}
	}
}
