using System;
using System.Collections.Generic;
using System.Linq;
using Ledger;
using Magistrate.Domain.Rules;
using Magistrate.Infrastructure;
using Magistrate.ReadModels;

namespace Magistrate.Domain
{
	public class Store
	{
		private readonly AggregateStore<Guid> _store;
		private readonly Projection _projections;
		private readonly List<IRule<User>> _userRules;
		private readonly List<IRule<Role>> _roleRules;
		private readonly List<IRule<Permission>> _permissionRules;

		public PermissionsReadModel Permissions { get; }
		public RolesReadModel Roles { get; }
		public UsersReadModel Users { get; }

		public Store(AggregateStore<Guid> store)
		{
			_store = store;
			_projections = new Projection();

			Permissions = new PermissionsReadModel();
			Roles = new RolesReadModel();
			Users = new UsersReadModel();

			RegisterProjections();

			_userRules = new List<IRule<User>>
			{
				new UniqueKeyRule<User>(Users.AllUsers)
			};

			_roleRules = new List<IRule<Role>>
			{
				new UniqueKeyRule<Role>(Roles.AllRoles)
			};

			_permissionRules = new List<IRule<Permission>>
			{
				new UniqueKeyRule<Permission>(Permissions.AllPermissions)
			};

		}

		private void RegisterProjections()
		{
			_projections.Register<Permission>(Permissions.ProjectTo);
			_projections.Register<Role>(Roles.ProjectTo);
			_projections.Register<User>(Users.ProjectTo);
		}

		public void LoadExistingReadModels()
		{
			// ?????????
		}

		public SaveResult Save(Permission permission)
		{
			var violatedRules = _permissionRules
				.Where(r => r.IsSatisfiedBy(permission) == false)
				.Select(r => r.GetMessage(permission))
				.ToList();

			if (violatedRules.Any())
				return SaveResult.Fail(violatedRules);

			_store.Save(permission);
			_projections.Run(permission);

			return SaveResult.Pass();
		}

		public SaveResult Save(Role role)
		{
			var violatedRules = _roleRules
				.Where(r => r.IsSatisfiedBy(role) == false)
				.Select(r => r.GetMessage(role))
				.ToList();

			if (violatedRules.Any())
				return SaveResult.Fail(violatedRules);

			_store.Save(role);
			_projections.Run(role);

			return SaveResult.Pass();
		}

		public SaveResult Save(User user)
		{
			var violatedRules = _userRules
				.Where(r => r.IsSatisfiedBy(user) == false)
				.Select(r => r.GetMessage(user))
				.ToList();

			if (violatedRules.Any())
				return SaveResult.Fail(violatedRules);

			_store.Save(user);
			_projections.Run(user);

			return SaveResult.Pass();
		}
	}
}
