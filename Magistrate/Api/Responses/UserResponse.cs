using System.Collections.Generic;
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
	}

	public class PermissionInspectorResponse
	{
		public IEnumerable<PermissionResponse> All { get; set; }
		public IEnumerable<PermissionResponse> Includes { get; set; }
		public IEnumerable<PermissionResponse> Revokes { get; set; }
	}

	public class RoleResponse
	{
		public string Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public IEnumerable<PermissionResponse> Permissions { get; set; }
	}

	public class PermissionResponse
	{
		public string Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
