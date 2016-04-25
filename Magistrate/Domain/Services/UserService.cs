using System.Collections.Generic;
using Magistrate.Domain.Events.UserEvents;

namespace Magistrate.Domain.Services
{
	public class UserService : ProjectionService
	{
		private readonly HashSet<UserKey> _keys;

		public UserService()
		{
			_keys = new HashSet<UserKey>();

			Register<UserCreatedEvent>(e => _keys.Add(e.Key));
		}

		public bool CanCreateUser(UserKey key)
		{
			return _keys.Contains(key) == false;
		}
	}
}
