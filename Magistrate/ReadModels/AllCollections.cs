using System;
using System.Collections.Generic;
using Ledger;
using Ledger.Infrastructure;
using Magistrate.Domain.Events.PermissionEvents;
using Magistrate.Domain.Events.RoleEvents;
using Magistrate.Domain.Events.UserEvents;
using Magistrate.Domain.Services;
using Magistrate.Infrastructure;

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

			_projections.Register<PermissionCreatedEvent>(e => _permissions[e.AggregateID] = PermissionModel.From(e));
			_projections.Register<PermissionDescriptionChangedEvent>(e => _permissions[e.AggregateID].Description = e.NewDescription);
			_projections.Register<PermissionNameChangedEvent>(e => _permissions[e.AggregateID].Name = e.NewName);

			_projections.Register<RoleCreatedEvent>(e => _roles[e.AggregateID] = RoleModel.From(e));
			_projections.Register<RoleDescriptionChangedEvent>(e => _roles[e.AggregateID].Description = e.NewDescription);
			_projections.Register<RoleNameChangedEvent>(e => _roles[e.AggregateID].Name = e.NewName);
			_projections.Register<PermissionAddedToRoleEvent>(e => _roles[e.AggregateID].Permissions.Add(_permissions[e.PermissionID]));
			_projections.Register<PermissionRemovedFromRoleEvent>(e => _roles[e.AggregateID].Permissions.Remove(_permissions[e.PermissionID]));

			_projections.Register<UserCreatedEvent>(e => _users[e.AggregateID] = UserModel.From(e));
			_projections.Register<UserNameChangedEvent>(e => _users[e.AggregateID].Name = e.NewName);
			_projections.Register<IncludeAddedToUserEvent>(e => _users[e.AggregateID].Includes.Add(_permissions[e.PermissionID]));
			_projections.Register<IncludeRemovedFromUserEvent>(e => _users[e.AggregateID].Includes.Remove(_permissions[e.PermissionID]));

			_projections.Register<RevokeAddedToUserEvent>(e => _users[e.AggregateID].Revokes.Add(_permissions[e.PermissionID]));
			_projections.Register<RevokeRemovedFromUserEvent>(e => _users[e.AggregateID].Revokes.Remove(_permissions[e.PermissionID]));
			_projections.Register<RoleAddedToUserEvent>(e => _users[e.AggregateID].Roles.Add(_roles[e.RoleID]));
			_projections.Register<RoleRemovedFromUserEvent>(e => _users[e.AggregateID].Roles.Remove(_roles[e.RoleID]));

			_projections.Register<PermissionDeactivatedEvent>(e =>
			{
				var permission = _permissions[e.AggregateID];

				_permissions.Remove(e.AggregateID);
				_roles.Values.ForEach(r => r.Permissions.Remove(permission));
				_users.Values.ForEach(u => u.Includes.Remove(permission));
				_users.Values.ForEach(u => u.Revokes.Remove(permission));
			});

			_projections.Register<RoleDeactivatedEvent>(e =>
			{
				var role = _roles[e.AggregateID];

				_roles.Remove(e.AggregateID);
				_users.Values.ForEach(u => u.Roles.Remove(role));
			});

			_projections.Register<UserDeactivatedEvent>(e =>
			{
				_users.Remove(e.AggregateID);
			});

		}

		public void Project(DomainEvent<Guid> e)
		{
			_projections.Apply(e);
		}

	}
}
