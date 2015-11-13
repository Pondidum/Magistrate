using System;
using System.Collections.Generic;
using System.Linq;
using Magistrate.Domain;

namespace Magistrate.ReadModels
{
	public class PermissionsReadModel
	{
		public IEnumerable<Permission> AllPermissions => _permissions;

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

		public Permission ByID(Guid id)
		{
			return _permissions.FirstOrDefault(p => p.ID == id);
		}
	}
}
