using System;
using System.Collections.Generic;
using System.Linq;
using Magistrate.Domain;

namespace Magistrate.ReadModels
{
	public class PermissionsReadModel
	{
		public Dictionary<string, string> AllPermissions => _permissions.ToDictionary(p => p.Key, p => p.Name);

		private readonly HashSet<Permission> _permissions;

		public PermissionsReadModel()
		{
			_permissions = new HashSet<Permission>();
		}

		public void ProjectTo(Permission permission)
		{
			_permissions.Add(permission);
		}

		public Permission ByKey(string key)
		{
			return _permissions.FirstOrDefault(p => string.Equals(p.Key, key, StringComparison.OrdinalIgnoreCase));
		}
	}
}
