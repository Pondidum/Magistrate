using System.Collections.Generic;
using System.Linq;

namespace Magistrate.Domain
{
	public class PermissionInspector
	{
		private readonly HashSet<Role> _roles;
		private readonly HashSet<Permission> _includes;
		private readonly HashSet<Permission> _revokes;
		private readonly HashSet<Permission> _all;

		public PermissionInspector(HashSet<Role> roles, HashSet<Permission> includes, HashSet<Permission> revokes)
		{
			_roles = roles;
			_includes = includes;
			_revokes = revokes;

			_all = new HashSet<Permission>();
			_all.UnionWith(_roles.SelectMany(r => r.Permissions));
			_all.UnionWith(_includes);
			_all.ExceptWith(_revokes);
		}

		public IEnumerable<Permission> Includes => _includes;
		public IEnumerable<Permission> Revokes => _revokes;
		public IEnumerable<Role> Roles => _roles;

		public bool Can(Permission permission)
		{
			return _all.Contains(permission);
		}
	}
}
