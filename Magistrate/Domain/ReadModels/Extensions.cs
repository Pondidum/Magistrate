using System;
using System.Collections.Generic;

namespace Magistrate.Domain.ReadModels
{
	internal static class Extensions
	{
		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> collection)
		{
			return new HashSet<T>(collection);
		}

		public static void AddRange<TValue>(this Dictionary<Guid, TValue> collection, IEnumerable<TValue> items)
			where TValue : IIdentity
		{
			foreach (var item in items)
			{
				collection[item.ID] = item;
			}
		}
	}
}