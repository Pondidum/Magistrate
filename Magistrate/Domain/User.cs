using System;
using Ledger;
using Magistrate.Domain.Events;

namespace Magistrate.Domain
{
	public class User : AggregateRoot<Guid>
	{
		public string Key { get; private set; }
		public string Name { get; private set; }

		public static User Create(string key, string name)
		{
			ValidateKey(key);
			ValidateName(name);

			var user = new User();
			user.ApplyEvent(new UserCreatedEvent
			{
				ID = Guid.NewGuid(),
				Key = key,
				Name = name
			});

			return user;
		}

		public void ChangeName(string name)
		{
			ValidateName(name);
			ApplyEvent(new NameChangedEvent { NewName = name });
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

		private void Handle(NameChangedEvent e)
		{
			Name = e.NewName;
		}

	}
}
