using System;
using System.Collections.Generic;
using Ledger;
using Magistrate.Domain.Events;

namespace Magistrate.Domain
{
	public class Role : AggregateRoot<Guid>, IKeyed
	{
		public string Key { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }

		public IEnumerable<Guid> Permissions => _permissions;


		private readonly HashSet<Guid> _permissions;

		private Role()
		{
			_permissions = new HashSet<Guid>();
		}

		public static Role Blank()
		{
			return new Role();
		}

		public static Role Create(MagistrateUser user, string key, string name, string description)
		{
			ValidateKey(key);
			ValidateName(name);

			var role = new Role();
			role.ApplyEvent(new RoleCreatedEvent
			{
				ID = Guid.NewGuid(),
				User = user,
				Key = key,
				Name = name,
				Description = description
			});

			return role;
		}

		public void ChangeName(MagistrateUser user, string name)
		{
			ValidateName(name);

			ApplyEvent(new RoleNameChangedEvent
			{
				User = user,
				NewName = name
			});
		}

		public void ChangeDescription(MagistrateUser user, string description)
		{
			ApplyEvent(new RoleDescriptionChangedEvent
			{
				User = user,
				NewDescription = description
			});
		}

		public void AddPermission(MagistrateUser user, Permission permission)
		{
			ApplyEvent(new PermissionAddedToRoleEvent
			{
				User = user,
				PermissionID = permission.ID
			});
		}

		public void RemovePermission(MagistrateUser user, Permission permission)
		{
			ApplyEvent(new PermissionRemovedFromRoleEvent
			{
				User = user,
				PermissionID = permission.ID
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
