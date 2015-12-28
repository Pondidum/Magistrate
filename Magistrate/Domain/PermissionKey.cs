using System;
using Newtonsoft.Json;

namespace Magistrate.Domain
{
	[JsonConverter(typeof(PermissionKeyConverter))]
	public class PermissionKey : IEquatable<PermissionKey>
	{
		private readonly string _key;

		public PermissionKey(string key)
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

			return Equals((PermissionKey)obj);
		}

		public bool Equals(PermissionKey other)
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

		public static bool operator ==(PermissionKey left, PermissionKey right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(PermissionKey left, PermissionKey right)
		{
			return !Equals(left, right);
		}
	}
}