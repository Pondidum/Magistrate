using System;
using System.Collections.Generic;
using System.Linq;

namespace Magistrate.Domain.Rules
{
	public class UniqueUserKeyRule : IRule<User>
	{
		private readonly IEnumerable<User> _allUsers;

		public UniqueUserKeyRule(IEnumerable<User> allUsers)
		{
			_allUsers = allUsers;
		}

		public bool IsSatisfiedBy(User target)
		{
			return _allUsers.Any(u => u.Key.Equals(target.Key, StringComparison.OrdinalIgnoreCase)) == false;
		}

		public string GetMessage(User target)
		{
			return $"There is already a User with the Key '{target.Key}'";
		}
	}
}