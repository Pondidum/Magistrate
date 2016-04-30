using System;
using System.Collections.Generic;
using Magistrate.Domain.Events.PermissionEvents;

namespace Magistrate.Domain.Services
{
	public class PermissionService : ProjectionService
	{
		private readonly Dictionary<Guid, PermissionKey> _keys;

		public PermissionService()
		{
			_keys = new Dictionary<Guid, PermissionKey>();

			Register<PermissionCreatedEvent>(e => _keys.Add(e.AggregateID, e.Key));
			Register<PermissionDeactivatedEvent>(e => _keys.Remove(e.AggregateID));
		}

		public bool CanCreatePermission(PermissionKey key)
		{
			return _keys.ContainsValue(key) == false;
		}
	}
}
