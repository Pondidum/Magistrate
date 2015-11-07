using System;
using System.Collections.Generic;
using System.Linq;

namespace Magistrate.Domain.Rules
{
	public class UniqueKeyRule<T> : IRule<T> where T : IKeyed
	{
		private readonly IEnumerable<T> _allItems;

		public UniqueKeyRule(IEnumerable<T> allItems)
		{
			_allItems = allItems;
		}

		public bool IsSatisfiedBy(T target)
		{
			return _allItems.Any(item => item.Key.Equals(target.Key, StringComparison.OrdinalIgnoreCase)) == false;
		}

		public string GetMessage(T target)
		{
			return $"There is already a User with the Key '{target.Key}'";
		}
	}
}
