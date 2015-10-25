using System;
using System.Collections.Generic;
using System.Linq;
using Magistrate.Domain;

namespace Magistrate.ReadModels
{
	public class PermissionsReadModel
	{
		public Dictionary<Guid, string> AllPermissions { get; }

		public PermissionsReadModel()
		{
			AllPermissions = new Dictionary<Guid, string>();
		}

		public void ProjectTo(Permission permission)
		{
			AllPermissions[permission.ID] = permission.Name;
		}
	}
}
