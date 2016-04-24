using System;
using System.Collections.Generic;
using Ledger;
using Magistrate.Domain.Events.UserEvents;
using Magistrate.Infrastructure;

namespace Magistrate.Domain.Services
{
	public class UserService
	{
		private readonly Projector _projections;
		private readonly HashSet<UserKey> _keys;

		public UserService()
		{
			_keys = new HashSet<UserKey>();
			_projections = new Projector();

			_projections.Register<UserCreatedEvent>(e => _keys.Add(e.Key));
		}

		public void Project(DomainEvent<Guid> e)
		{
			_projections.Apply(e);
		}

		public bool CanCreateUser(UserKey key)
		{
			return _keys.Contains(key) == false;
		}
	}
}
