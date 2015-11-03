using System;
using System.Collections.Generic;
using System.Linq;
using Magistrate.Domain;

namespace Magistrate.ReadModels
{
	public class RolesReadModel
	{
		public IEnumerable<Role> AllRoles => _roles;

		private readonly HashSet<Role> _roles;

		public RolesReadModel()
		{
			_roles = new HashSet<Role>();
		}

		public void ProjectTo(Role role)
		{
			_roles.Add(role);
		}

		public Role ByKey(string key)
		{
			return _roles.FirstOrDefault(r => string.Equals(r.Key, key, StringComparison.OrdinalIgnoreCase));
		}

		public Role ByID(Guid id)
		{
			return _roles.FirstOrDefault(r => r.ID == id);
		}
	}
}
