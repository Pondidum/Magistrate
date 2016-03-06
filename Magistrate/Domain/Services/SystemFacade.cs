using System;
using System.Collections.Generic;
using System.Linq;
using Ledger;
using Ledger.Infrastructure;
using Magistrate.Domain.ReadModels;
using Magistrate.Domain.Rules;

namespace Magistrate.Domain.Services
{
	public class SystemFacade
	{
		public const string MagistrateStream = "Magistrate";

		public IEnumerable<UserReadModel> Users => _projections.Users;
		public IEnumerable<RoleReadModel> Roles => _projections.Roles;
		public IEnumerable<PermissionReadModel> Permissions => _projections.Permissions;
		public IEnumerable<HistoryReadModel> History => _projections.History;

		public UserService UserService => _userService;

		private readonly AggregateStore<Guid> _store;
		private readonly ReadModelProjections _projections;
		private readonly UserService _userService;

		public SystemFacade(IEventStore eventStore)
		{
			_projections = new ReadModelProjections();
			var es = new ProjectionEventStore(eventStore, _projections.Project);

			_store = new AggregateStore<Guid>(es);

			_userService = new UserService(_projections.Users);
		}

		public void Load()
		{
			_store
				.ReplayAll(MagistrateStream)
				.ForEach(_projections.Project);
		}

		public UserReadModel CreateUser(MagistrateUser currentUser, UserKey key, string name)
		{
			var user = User.Create(_userService, currentUser, key, name);
			var model = new UserReadModel { ID = user.ID, Key = user.Key };

			_store.Save(MagistrateStream, user);

			return Users.First(u => u.ID == user.ID);
		}

		public RoleReadModel CreateRole(MagistrateUser currentUser, RoleKey key, string name, string description)
		{
			var role = Role.Create(currentUser, key, name, description);
			var model = new RoleReadModel { ID = role.ID, Key = role.Key };

			CheckRules<RoleReadModel, RoleKey>(Roles, model);

			_store.Save(MagistrateStream, role);

			return Roles.First(r => r.ID == role.ID);
		}

		public PermissionReadModel CreatePermission(MagistrateUser currentUser, PermissionKey key, string name, string description)
		{
			var permission = Permission.Create(currentUser, key, name, description);
			var model = new PermissionReadModel { ID = permission.ID, Key = permission.Key };

			CheckRules<PermissionReadModel, PermissionKey>(Permissions, model);

			_store.Save(MagistrateStream, permission);

			return Permissions.First(p => p.ID == permission.ID);
		}

		public void OnPermission(PermissionKey key, Action<Permission> action)
		{
			var permission = LoadPermission(key);

			action(permission);

			_store.Save(MagistrateStream, permission);
		}

		public void OnRole(RoleKey key, Action<Role> action)
		{
			var role = LoadRole(key);

			action(role);

			_store.Save(MagistrateStream, role);
		}

		public void OnUser(UserKey key, Action<User> action)
		{
			var user = LoadUser(key);

			action(user);

			_store.Save(MagistrateStream, user);
		}

		public Permission LoadPermission(PermissionKey key)
		{
			var model = Permissions.First(p => p.Key == key);
			return _store.Load(MagistrateStream, model.ID, Permission.Blank);
		}

		public Role LoadRole(RoleKey key)
		{
			var model = Roles.First(r => r.Key == key);
			return _store.Load(MagistrateStream, model.ID, Role.Blank);
		}

		public User LoadUser(UserKey key)
		{
			var model = Users.First(r => r.Key == key);
			return _store.Load(MagistrateStream, model.ID, User.Blank);
		}

		private IEnumerable<string> GetRuleViolations<T, TKey>(IEnumerable<T> collection, T target) where T : IKeyed<TKey>, IIdentity
		{
			var rules = new[] { new UniqueKeyRule<T, TKey>(collection) };

			return rules
				.Where(r => r.IsSatisfiedBy(target) == false)
				.Select(r => r.GetMessage(target))
				.ToList();
		}

		private void CheckRules<T, TKey>(IEnumerable<T> collection, T target) where T : IKeyed<TKey>, IIdentity
		{
			var violations = GetRuleViolations<T, TKey>(collection, target).ToList();

			if (violations.Any())
				throw new RuleViolationException<TKey>(target, violations);
		}
	}
}
