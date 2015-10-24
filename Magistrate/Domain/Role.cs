using System;
using Ledger;
using Magistrate.Domain.Events;

namespace Magistrate.Domain
{
	public class Role : AggregateRoot<Guid>
	{
		public string Key { get; private set; }
		public string Name { get; private set; } 
		public string Description { get; private set; }

		public static Role Create(string key, string name, string description)
		{
			ValidateKey(key);
			ValidateName(name);

			var role = new Role();
			role.ApplyEvent(new RoleCreatedEvent
			{
				ID = Guid.NewGuid(),
				Key = key,
				Name = name,
				Description = description
			});

			return role;
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

		private void Handle(RoleCreatedEvent e)
		{
			ID = e.ID;
			Key = e.Key;
			Name = e.Name;
			Description = e.Description;
		}

		private void Handle(NameChangedEvent e)
		{
			Name = e.NewName;
		}

		private void Handle(DescriptionChangedEvent e)
		{
			Description = e.NewDescription;
		}
	}
}
