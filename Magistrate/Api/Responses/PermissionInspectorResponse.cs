using System.Collections.Generic;

namespace Magistrate.Api.Responses
{
	public class PermissionInspectorResponse
	{
		public IEnumerable<PermissionResponse> All { get; set; }
		public IEnumerable<PermissionResponse> Includes { get; set; }
		public IEnumerable<PermissionResponse> Revokes { get; set; }
	}
}