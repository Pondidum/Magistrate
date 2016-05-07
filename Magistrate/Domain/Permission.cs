using System;
using System.Security;
using Ledger;
using Magistrate.Domain.Events;
using Magistrate.Domain.Events.PermissionEvents;
using Magistrate.Domain.Services;

namespace Magistrate.Domain
{
	public class Permission : AggregateRoot<Guid>, IEquatable<Permission>, IIdentity
	{
		public PermissionKey Key { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
		public bool IsActive { get; private set; }

		private Permission()
		{
			IsActive = true;
		}

		public static Permission Blank()
		{
			return new Permission();
		}

		public static Permission Create(PermissionService service, Operator user, PermissionKey key, string name, string description)
		{
			if (user.CanCreatePermissions== false) throw new SecurityException($"{user.Name} cannot create permissions.");

			if (service.CanCreatePermission(key) == false)
				throw new ArgumentException($"There is already a Permission with the Key '{key}'", nameof(key));

			ValidateName(name);

			var perm = new Permission();
			perm.ApplyEvent(new PermissionCreatedEvent
			(
				user,
				Guid.NewGuid(),
				key,
				name,
				description
			));

			return perm;
		}


		public void ChangeName(Operator user, string newName)
		{
			ValidateName(newName);

			if (Name.Equals(newName, StringComparison.OrdinalIgnoreCase))
				return;

			ApplyEvent(new PermissionNameChangedEvent
			(
				user,
				newName
			));
		}

		public void ChangeDescription(Operator user, string newDescription)
		{
			if (Description.Equals(newDescription, StringComparison.OrdinalIgnoreCase))
				return;

			ApplyEvent(new PermissionDescriptionChangedEvent
			(
				user,
				newDescription
			));
		}

		public void Delete(Operator user)
		{
			ApplyEvent(new PermissionDeletedEvent
			(
				user
			));
		}

		private static void ValidateName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Name cannot be null or whitespace", nameof(name));
		}



		private void Handle(PermissionCreatedEvent e)
		{
			ID = e.ID;
			Key = e.Key;
			Name = e.Name;
			Description = e.Description;
		}

		private void Handle(PermissionDescriptionChangedEvent e)
		{
			Description = e.NewDescription;
		}

		private void Handle(PermissionNameChangedEvent e)
		{
			Name = e.NewName;
		}

		private void Handle(PermissionDeletedEvent e)
		{
			IsActive = false;
		}




		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;

			return Equals((Permission)obj);
		}

		public bool Equals(Permission other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			return Equals(ID, other.ID);
		}

		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}

		public static bool operator ==(Permission left, Permission right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Permission left, Permission right)
		{
			return !Equals(left, right);
		}
	}
}
