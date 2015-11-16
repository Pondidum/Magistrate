using System.Collections.Generic;
using System.Linq;

namespace Magistrate.Api.Responses
{
	public class UserResponse
	{
		public string Key { get; set; }
		public string Name { get; set; }
		public bool IsActive { get; set; }

		public IEnumerable<PermissionResponse> Includes { get; set; }
		public IEnumerable<PermissionResponse> Revokes { get; set; }
		public IEnumerable<RoleResponse> Roles { get; set; }

		public UserResponse()
		{
			Includes = Enumerable.Empty<PermissionResponse>();
			Revokes = Enumerable.Empty<PermissionResponse>();
			Roles = Enumerable.Empty<RoleResponse>();
		}
	}
}
