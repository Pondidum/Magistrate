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
				new UniqueUserKeyRule(Users.AllUsers)
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

		public void Save(Permission permission)
		{
			_store.Save(permission);
			_projections.Run(permission);
		}

		public void Save(Role role)
		{
			_store.Save(role);
			_projections.Run(role);
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
