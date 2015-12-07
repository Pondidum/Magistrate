using System.Collections.Generic;

namespace Magistrate.Domain.ReadModels
{
	internal static class Extensions
	{
		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> collection)
		{
			return new HashSet<T>(collection);
		}
	}
}