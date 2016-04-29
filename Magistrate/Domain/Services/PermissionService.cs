using System.Collections.Generic;
using Magistrate.Domain.Events.PermissionEvents;

namespace Magistrate.Domain.Services
{
	public class PermissionService : ProjectionService
	{
		private readonly HashSet<PermissionKey> _keys;

		public PermissionService()
		{
			_keys = new HashSet<PermissionKey>();

			Register<PermissionCreatedEvent>(e => _keys.Add(e.Key));
			Register<PermissionDeactivatedEvent>(e => _keys.Remove(e.));
		}

		public bool CanCreatePermission(PermissionKey key)
		{
			return _keys.Contains(key) == false;
		}
	}
}
