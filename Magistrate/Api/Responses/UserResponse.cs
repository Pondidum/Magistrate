using System.Collections.Generic;
using System.Linq;
using Magistrate.Domain;

namespace Magistrate.Api.Responses
{
	public class UserResponse
	{
		public string Key { get; set; }
		public string Name { get; set; }
		public bool IsActive { get; set; }

		public PermissionInspector Permissions { get; set; }
		public IEnumerable<RoleResponse> Roles { get; set; }

		public UserResponse()
		{
			Roles = Enumerable.Empty<RoleResponse>();
		}
	}
}
