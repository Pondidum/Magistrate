using System;
using Newtonsoft.Json;

namespace Magistrate.Domain
{
	[JsonConverter(typeof(UserKeyConverter))]
	public class UserKey : IEquatable<UserKey>
	{
		private readonly string _key;

		public UserKey(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentException("Key cannot be null or whitespace", nameof(key));

			_key = key;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;

			return Equals((UserKey)obj);
		}

		public bool Equals(UserKey other)
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

		public static bool operator ==(UserKey left, UserKey right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(UserKey left, UserKey right)
		{
			return !Equals(left, right);
		}
	}
}
