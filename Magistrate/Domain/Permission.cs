using System;
using Ledger;
using Magistrate.Domain.Events;

namespace Magistrate.Domain
{
	public class Permission : AggregateRoot<Guid>, IEquatable<Permission>, IKeyed
	{
		public string Key { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }

		public static Permission Blank()
		{
			return new Permission();
		}

		public static Permission Create(string key, string name, string description)
		{
			ValidateKey(key);
			ValidateName(name);

			var perm = new Permission();
			perm.ApplyEvent(new PermissionCreatedEvent
			{
				ID = Guid.NewGuid(),
				Key = key,
				Name = name,
				Description = description
			});

			return perm;
		}


		public void ChangeName(string name)
		{
			ValidateName(name);

			ApplyEvent(new NameChangedEvent
			{
				NewName = name
			});
		}

		public void ChangeDescription(string description)
		{
			ApplyEvent(new DescriptionChangedEvent
			{
				NewDescription = description
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

		private void Handle(DescriptionChangedEvent e)
		{
			Description = e.NewDescription;
		}

		private void Handle(NameChangedEvent e)
		{
			Name = e.NewName;
		}



		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;

			return Equals((Permission) obj);
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
