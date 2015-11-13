using System;
using System.Collections.Generic;
using System.Linq;
using Ledger;

namespace Magistrate.Domain.Rules
{
	public class UniqueKeyRule<T> : IRule<T> where T : AggregateRoot<Guid>, IKeyed
	{
		private readonly IEnumerable<T> _allItems;

		public UniqueKeyRule(IEnumerable<T> allItems)
		{
			_allItems = allItems;
		}

		public bool IsSatisfiedBy(T target)
		{
			return _allItems
				.Where(item => item.ID != target.ID)
				.Any(item => item.Key.Equals(target.Key, StringComparison.OrdinalIgnoreCase)) == false;
		}

		public string GetMessage(T target)
		{
			return $"There is already a {target.GetType().Name} with the Key '{target.Key}'";
		}
	}
}
