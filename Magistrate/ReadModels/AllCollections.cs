using System;
using System.Collections.Generic;
using Ledger;
using Ledger.Infrastructure;
using Magistrate.Domain.Events.PermissionEvents;
using Magistrate.Domain.Events.RoleEvents;
using Magistrate.Domain.Events.UserEvents;
using Magistrate.Domain.Services;

namespace Magistrate.ReadModels
{
	public class AllCollections
	{
		private readonly Dictionary<Guid, UserModel> _users;
		private readonly Dictionary<Guid, RoleModel> _roles;
		private readonly Dictionary<Guid, PermissionModel> _permissions;

		private readonly Projector _projections;

		public AllCollections()
		{
			_projections = new Projector();
			_users = new Dictionary<Guid, UserModel>();
			_roles = new Dictionary<Guid, RoleModel>();
			_permissions = new Dictionary<Guid, PermissionModel>();

			_projections.Add<PermissionCreatedEvent>(e => _permissions[e.AggregateID] = PermissionModel.From(e));
			_projections.Add<PermissionDescriptionChangedEvent>(e => _permissions[e.AggregateID].Description = e.NewDescription);
			_projections.Add<PermissionNameChangedEvent>(e => _permissions[e.AggregateID].Name = e.NewName);

			_projections.Add<RoleCreatedEvent>(e => _roles[e.AggregateID] = RoleModel.From(e));
			_projections.Add<RoleDescriptionChangedEvent>(e => _roles[e.AggregateID].Description = e.NewDescription);
			_projections.Add<RoleNameChangedEvent>(e => _roles[e.AggregateID].Name = e.NewName);
			_projections.Add<PermissionAddedToRoleEvent>(e => _roles[e.AggregateID].Permissions.Add(_permissions[e.PermissionID]));
			_projections.Add<PermissionRemovedFromRoleEvent>(e => _roles[e.AggregateID].Permissions.Remove(_permissions[e.PermissionID]));

			_projections.Add<UserCreatedEvent>(e => _users[e.AggregateID] = UserModel.From(e));
			_projections.Add<UserNameChangedEvent>(e => _users[e.AggregateID].Name = e.NewName);
			_projections.Add<IncludeAddedToUserEvent>(e => _users[e.AggregateID].Includes.Add(_permissions[e.PermissionID]));
			_projections.Add<IncludeRemovedFromUserEvent>(e => _users[e.AggregateID].Includes.Remove(_permissions[e.PermissionID]));

			_projections.Add<RevokeAddedToUserEvent>(e => _users[e.AggregateID].Revokes.Add(_permissions[e.PermissionID]));
			_projections.Add<RevokeRemovedFromUserEvent>(e => _users[e.AggregateID].Revokes.Remove(_permissions[e.PermissionID]));
			_projections.Add<RoleAddedToUserEvent>(e => _users[e.AggregateID].Roles.Add(_roles[e.RoleID]));
			_projections.Add<RoleRemovedFromUserEvent>(e => _users[e.AggregateID].Roles.Remove(_roles[e.RoleID]));

			_projections.Add<PermissionDeactivatedEvent>(e =>
			{
				var permission = _permissions[e.AggregateID];

				_permissions.Remove(e.AggregateID);
				_roles.Values.ForEach(r => r.Permissions.Remove(permission));
				_users.Values.ForEach(u => u.Includes.Remove(permission));
				_users.Values.ForEach(u => u.Revokes.Remove(permission));
			});

			_projections.Add<RoleDeactivatedEvent>(e =>
			{
				var role = _roles[e.AggregateID];

				_roles.Remove(e.AggregateID);
				_users.Values.ForEach(u => u.Roles.Remove(role));
			});

			_projections.Add<UserDeactivatedEvent>(e =>
			{
				_users.Remove(e.AggregateID);
			});

		}

		public void Project(DomainEvent<Guid> e)
		{
			_projections.Project(e);
		}

	}
}
