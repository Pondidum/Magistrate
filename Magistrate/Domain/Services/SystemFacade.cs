using System;
using System.Collections.Generic;
using System.Linq;
using Ledger;
using Magistrate.Domain.ReadModels;
using Magistrate.Domain.Rules;

namespace Magistrate.Domain.Services
{
	public class SystemFacade
	{
		private readonly MagistrateSystem _system;

		public IEnumerable<UserReadModel> Users => _projections.Users;
		public IEnumerable<RoleReadModel> Roles => _projections.Roles;
		public IEnumerable<PermissionReadModel> Permissions => _projections.Permissions;

		private readonly AggregateStore<Guid> _store;
		private readonly SystemProjections _projections;

		public SystemFacade(IEventStore eventStore)
		{
			_projections = new SystemProjections();
			var wrapped = new ProjectionEventStore(eventStore, _projections.Project);

			_store = new AggregateStore<Guid>(wrapped);
			_system = new MagistrateSystem();
		}

		public UserReadModel CreateUser(MagistrateUser currentUser, string key, string name)
		{
			var user = User.Create(currentUser, key, name);
			var model = new UserReadModel { ID = user.ID, Key = user.Key };

			CheckRules(Users, model);

			_system.AddUser(currentUser, user);

			_store.Save(user);
			_store.Save(_system);

			return Users.First(u => u.ID == user.ID);
		}

		public RoleReadModel CreateRole(MagistrateUser currentUser, string key, string name, string description)
		{
			var role = Role.Create(currentUser, key, name, description);
			var model = new RoleReadModel { ID = role.ID, Key = role.Key };

			CheckRules(Roles, model);

			_system.AddRole(currentUser, role);

			_store.Save(role);
			_store.Save(_system);

			return Roles.First(r => r.ID == role.ID);
		}

		public PermissionReadModel CreatePermission(MagistrateUser currentUser, string key, string name, string description)
		{
			var permission = Permission.Create(currentUser, key, name, description);
			var model = new PermissionReadModel { ID = permission.ID, Key = permission.Key };

			CheckRules(Permissions, model);

			_system.AddPermission(currentUser, permission);

			_store.Save(permission);
			_store.Save(_system);

			return Permissions.First(p => p.ID == permission.ID);
		}

		public void OnPermission(string key, Action<Permission> action)
		{
			var permission = LoadPermission(key);

			action(permission);

			_store.Save(permission);
		}

		public void OnRole(string key, Action<Role> action)
		{
			var role = LoadRole(key);

			action(role);

			_store.Save(role);
		}

		public void OnUser(string key, Action<User> action)
		{
			var user = LoadUser(key);

			action(user);

			_store.Save(user);
		}

		public Permission LoadPermission(string key)
		{
			var model = Permissions.First(p => p.Key == key);
			return _store.Load(model.ID, Permission.Blank);
		}

		public Role LoadRole(string key)
		{
			var model = Roles.First(r => r.Key == key);
			return _store.Load(model.ID, Role.Blank);
		}

		public User LoadUser(string key)
		{
			var model = Users.First(r => r.Key == key);
			return _store.Load(model.ID, User.Blank);
		}

		private void CheckRules<T>(IEnumerable<T> collection, T target) where T : IKeyed, IIdentity
		{
			var rules = new[] { new UniqueKeyRule<T>(collection) };

			var violations = rules
				.Where(r => r.IsSatisfiedBy(target) == false)
				.Select(r => r.GetMessage(target))
				.ToList();

			if (violations.Any())
				throw new RuleViolationException(target, violations);
		}
	}
}
