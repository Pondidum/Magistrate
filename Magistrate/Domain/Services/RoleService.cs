using System.Collections.Generic;
using Magistrate.Domain.Events.RoleEvents;

namespace Magistrate.Domain.Services
{
	public class RoleService : ProjectionService
	{
		private readonly HashSet<RoleKey> _keys;

		public RoleService()
		{
			_keys = new HashSet<RoleKey>();

			Register<RoleCreatedEvent>(e => _keys.Add(e.Key));
		}

		public bool CanCreateRole(RoleKey key)
		{
			return _keys.Contains(key) == false;
		}
	}
}
