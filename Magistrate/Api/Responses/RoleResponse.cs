using System.Collections.Generic;

namespace Magistrate.Api.Responses
{
	public class RoleResponse
	{
		public string Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public IEnumerable<PermissionResponse> Permissions { get; set; }
	}
}