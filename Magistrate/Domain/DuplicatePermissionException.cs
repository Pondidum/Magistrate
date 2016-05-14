using System;

namespace Magistrate.Domain
{
	public class DuplicatePermissionException : Exception
	{
		public DuplicatePermissionException(PermissionKey key)
			: base($"There is already a Permission with the key '{key}'")
		{
		}
	}

	public class DuplicateRoleException : Exception
	{
		public DuplicateRoleException(RoleKey key)
			: base($"There is already a Role with the key '{key}'")
		{
		}
	}

	public class DuplicateUserException : Exception
	{
		public DuplicateUserException(UserKey key)
			: base($"There is already a User with the key '{key}'")
		{
		}
	}
}
