﻿using System;
using Ledger;
using Magistrate.Domain.Events;
using Magistrate.Domain.Events.PermissionEvents;

namespace Magistrate.Domain
{
	public class Permission : AggregateRoot<Guid>, IEquatable<Permission>, IKeyed, IIdentity
	{
		public string Key { get; private set; }
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

		public static Permission Create(MagistrateUser user, string key, string name, string description)
		{
			ValidateKey(key);
			ValidateName(name);

			var perm = new Permission();
			perm.ApplyEvent(new PermissionCreatedEvent
			{
				ID = Guid.NewGuid(),
				User = user,
				Key = key,
				Name = name,
				Description = description
			});

			return perm;
		}


		public void ChangeName(MagistrateUser user, string newName)
		{
			ValidateName(newName);

			if (Name.Equals(newName, StringComparison.OrdinalIgnoreCase))
				return;

			ApplyEvent(new PermissionNameChangedEvent
			{
				User = user,
				NewName = newName
			});
		}

		public void ChangeDescription(MagistrateUser user, string newDescription)
		{
			if (Description.Equals(newDescription, StringComparison.OrdinalIgnoreCase))
				return;

			ApplyEvent(new PermissionDescriptionChangedEvent
			{
				User = user,
				NewDescription = newDescription
			});
		}

		public void Deactivate(MagistrateUser user)
		{
			ApplyEvent(new PermissionDeactivatedEvent
			{
				User = user
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

		private void Handle(PermissionDeactivatedEvent e)
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
