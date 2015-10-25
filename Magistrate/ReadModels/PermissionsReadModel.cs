using System;
using System.Collections.Generic;
using Magistrate.Domain;

namespace Magistrate.ReadModels
{
	public class PermissionsReadModel
	{
		public IEnumerable<KeyValuePair<Guid, string>> AllPermissions => _permissions;

		private readonly Dictionary<Guid, string> _permissions;

		public PermissionsReadModel()
		{
			_permissions = new Dictionary<Guid, string>();
		}

		public void ProjectTo(Permission permission)
		{
			_permissions[permission.ID] = permission.Name;
		}
	}
}
