using System;
using System.Collections.Generic;
using System.Linq;
using Magistrate.Domain.ReadModels;
using Magistrate.Domain.Rules;

namespace Magistrate.Domain.Services
{
	public class UserService
	{
		private readonly IEnumerable<UserReadModel> _users;

		public UserService(IEnumerable<UserReadModel> users)
		{
			_users = users;
		}

		public bool CanCreateUser(UserKey key)
		{
			var model = new UserReadModel { ID = Guid.NewGuid(), Key = key };

			return GetRuleViolations(model).Any() == false;
		}

		private IEnumerable<string> GetRuleViolations(UserReadModel target)
		{
			var rules = new[] { new UniqueKeyRule<UserReadModel, UserKey>(_users) };

			return rules
				.Where(r => r.IsSatisfiedBy(target) == false)
				.Select(r => r.GetMessage(target))
				.ToList();
		}

		public void AssertCanCreateUser(UserKey key)
		{
			var model = new UserReadModel { ID = Guid.NewGuid(), Key = key };
			var violations = GetRuleViolations(model).ToList();

			if (violations.Any())
				throw new RuleViolationException<UserKey>(model, violations);
		}
	}
}
