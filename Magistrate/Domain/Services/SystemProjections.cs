using System;
using System.Collections.Generic;
using System.Linq;
using Ledger;
using Ledger.Infrastructure;
using Magistrate.Domain.Events.PermissionEvents;
using Magistrate.Domain.Events.RoleEvents;
using Magistrate.Domain.Events.SystemEvents;
using Magistrate.Domain.Events.UserEvents;
using Magistrate.Domain.ReadModels;

namespace Magistrate.Domain.Services
{
	public class SystemProjections
	{
		public IEnumerable<UserReadModel> Users => _users.Values;
		public IEnumerable<RoleReadModel> Roles => _roles.Values;
		public IEnumerable<PermissionReadModel> Permissions => _permissions.Values;

		private readonly Dictionary<Guid, UserReadModel> _users;
		private readonly Dictionary<Guid, RoleReadModel> _roles;
		private readonly Dictionary<Guid, PermissionReadModel> _permissions;

		private readonly  Projector _projector;

		public SystemProjections()
		{
			_projector = new Projector();

			_users = new Dictionary<Guid, UserReadModel>();
			_roles = new Dictionary<Guid, RoleReadModel>();
			_permissions = new Dictionary<Guid, PermissionReadModel>();

			RegisterProjections(_projector);
		}

		public void Project(IDomainEvent<Guid> @event)
		{
			_projector.Project(@event);
		}

		public void Rebuild(IEnumerable<Permission> permissions, IEnumerable<Role> roles, IEnumerable<User> users)
		{
			_permissions.AddRange(permissions.Where(p => p.IsActive).Select(PermissionReadModel.From));
			_roles.AddRange(roles.Where(r => r.IsActive).Select(r => RoleReadModel.From(r, _permissions)));
			_users.AddRange(users.Where(u => u.IsActive).Select(u => UserReadModel.From(u, _roles, _permissions)));
		}

		private void RegisterProjections(Projector projector)
		{
			projector.Add<PermissionCreatedEvent>(e => _permissions[e.AggregateID] = PermissionReadModel.From(e));
			projector.Add<PermissionDescriptionChangedEvent>(e => _permissions[e.AggregateID].Description = e.NewDescription);
			projector.Add<PermissionNameChangedEvent>(e => _permissions[e.AggregateID].Name = e.NewName);

			projector.Add<RoleCreatedEvent>(e => _roles[e.AggregateID] = RoleReadModel.From(e));
			projector.Add<RoleDescriptionChangedEvent>(e => _roles[e.AggregateID].Description = e.NewDescription);
			projector.Add<RoleNameChangedEvent>(e => _roles[e.AggregateID].Name = e.NewName);
			projector.Add<PermissionAddedToRoleEvent>(e => _roles[e.AggregateID].Permissions.Add(_permissions[e.PermissionID]));
			projector.Add<PermissionRemovedFromRoleEvent>(e => _roles[e.AggregateID].Permissions.Remove(_permissions[e.PermissionID]));

			projector.Add<UserCreatedEvent>(e => _users[e.AggregateID] = UserReadModel.From(e));
			projector.Add<UserNameChangedEvent>(e => _users[e.AggregateID].Name = e.NewName);
			projector.Add<IncludeAddedToUserEvent>(e =>
			{
				var permission = _permissions[e.PermissionID];
				var user = _users[e.AggregateID];

				user.Includes.Add(permission);
				user.Revokes.Remove(permission);
			});

			projector.Add<IncludeRemovedFromUserEvent>(e => _users[e.AggregateID].Includes.Remove(_permissions[e.PermissionID]));
			projector.Add<RevokeAddedToUserEvent>(e =>
			{
				var permission = _permissions[e.PermissionID];
				var user = _users[e.AggregateID];

				user.Includes.Remove(permission);
				user.Revokes.Add(permission);
			});

			projector.Add<RevokeRemovedFromUserEvent>(e => _users[e.AggregateID].Revokes.Remove(_permissions[e.PermissionID]));
			projector.Add<RoleAddedToUserEvent>(e => _users[e.AggregateID].Roles.Add(_roles[e.RoleID]));
			projector.Add<RoleRemovedFromUserEvent>(e => _users[e.AggregateID].Roles.Remove(_roles[e.RoleID]));

			projector.Add<PermissionDeactivatedEvent>(e =>
			{
				var permission = _permissions[e.AggregateID];

				_permissions.Remove(e.AggregateID);
				_roles.Values.ForEach(r => r.Permissions.Remove(permission));
				_users.Values.ForEach(u => u.Includes.Remove(permission));
				_users.Values.ForEach(u => u.Revokes.Remove(permission));
			});

			projector.Add<RoleDeactivatedEvent>(e =>
			{
				var role = _roles[e.AggregateID];

				_roles.Remove(e.AggregateID);
				_users.Values.ForEach(u => u.Roles.Remove(role));
			});

			projector.Add<UserDeactivatedEvent>(e =>
			{
				_users.Remove(e.AggregateID);
			});
		}

	}
}
