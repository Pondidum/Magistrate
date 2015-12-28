using System;
using Newtonsoft.Json;

namespace Magistrate.Domain
{
	[JsonConverter(typeof(RoleKeyConverter))]
	public class RoleKey : IEquatable<RoleKey>
	{
		private readonly string _key;

		public RoleKey(string key)
		{
			_key = key;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;

			return Equals((RoleKey)obj);
		}

		public bool Equals(RoleKey other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			return string.Equals(_key, other._key, StringComparison.OrdinalIgnoreCase);
		}

		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(_key);
		}

		public override string ToString()
		{
			return _key;
		}

		public static bool operator ==(RoleKey left, RoleKey right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(RoleKey left, RoleKey right)
		{
			return !Equals(left, right);
		}
	}
}
