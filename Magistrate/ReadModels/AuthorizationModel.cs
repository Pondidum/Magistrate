using System;
using System.Collections.Generic;
using System.Linq;
using Ledger;
using Magistrate.Domain.Events.RoleEvents;
using Magistrate.Domain.Events.UserEvents;
using Magistrate.Infrastructure;

namespace Magistrate.ReadModels
{
	public class AuthorizationModel
	{
		private readonly Projector _projections;
		private readonly Dictionary<Guid, UserAuthModel> _users;

		public AuthorizationModel()
		{
			_projections = new Projector();
			_users = new Dictionary<Guid, UserAuthModel>();

			var roles = new Dictionary<Guid, RoleAuthModel>();

			_projections.Register<UserCreatedEvent>(e => _users[e.ID] = new UserAuthModel(e.ID));
			_projections.Register<RoleCreatedEvent>(e => roles[e.ID] = new RoleAuthModel());

			_projections.Register<PermissionAddedToRoleEvent>(e => roles[e.AggregateID].Permissions.Add(e.PermissionID));
			_projections.Register<PermissionRemovedFromRoleEvent>(e => roles[e.AggregateID].Permissions.Remove(e.PermissionID));
;
			_projections.Register<IncludeAddedToUserEvent>(e => _users[e.AggregateID].Includes.Add(e.PermissionID));
			_projections.Register<IncludeRemovedFromUserEvent>(e => _users[e.AggregateID].Includes.Remove(e.PermissionID));

			_projections.Register<RevokeAddedToUserEvent>(e => _users[e.AggregateID].Revokes.Add(e.PermissionID));
			_projections.Register<RevokeRemovedFromUserEvent>(e => _users[e.AggregateID].Revokes.Remove(e.PermissionID));

			_projections.Register<RoleAddedToUserEvent>(e => _users[e.AggregateID].Roles.Add(roles[e.RoleID]));
			_projections.Register<RoleRemovedFromUserEvent>(e => _users[e.AggregateID].Roles.Remove(roles[e.RoleID]));
		}

		public void Project(DomainEvent<Guid> e)
		{
			_projections.Apply(e);
		}

		public bool CanUserPerformAction(Guid userID, Guid permissionID)
		{
			UserAuthModel user;
			if (_users.TryGetValue(userID, out user) == false)
				return false;

			if (user.Revokes.Contains(permissionID))
				return false;

			return user.Includes.Contains(permissionID) || user.Roles.Any(r => r.Permissions.Contains(permissionID));
		}

		private class UserAuthModel
		{
			public Guid ID { get; }
			public HashSet<Guid> Includes { get; }
			public HashSet<Guid> Revokes { get; }
			public List<RoleAuthModel> Roles { get; }

			public UserAuthModel(Guid id)
			{
				ID = id;
				Includes = new HashSet<Guid>();
				Revokes = new HashSet<Guid>();

				Roles = new List<RoleAuthModel>();
			}

			
		}

		private class RoleAuthModel
		{
			public HashSet<Guid> Permissions { get; }

			public RoleAuthModel()
			{
				Permissions = new HashSet<Guid>();
			}
		}
	}
}
