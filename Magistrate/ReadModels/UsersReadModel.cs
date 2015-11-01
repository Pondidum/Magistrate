using System;
using System.Collections.Generic;
using System.Linq;
using Magistrate.Domain;

namespace Magistrate.ReadModels
{
	public class UsersReadModel
	{
		public IEnumerable<User> ActiveUsers => _users.Where(u => u.IsActive);
		public IEnumerable<User> AllUsers => _users;

		private readonly HashSet<User> _users;

		public UsersReadModel()
		{
			_users = new HashSet<User>();
		}

		public void ProjectTo(User user)
		{
			_users.Add(user);
		}

		public User ByKey(string key)
		{
			return _users.FirstOrDefault(p => string.Equals(p.Key, key, StringComparison.OrdinalIgnoreCase));
		}
	}
}
