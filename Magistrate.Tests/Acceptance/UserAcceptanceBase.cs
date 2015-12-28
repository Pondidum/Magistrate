using System;
using System.Linq;
using Ledger;
using Ledger.Stores;
using Magistrate.Domain;
using Magistrate.Domain.ReadModels;
using Magistrate.Domain.Services;

namespace Magistrate.Tests.Acceptance
{
	public class UserAcceptanceBase
	{
		protected User User { get; }
		protected Role TestRole { get; }
		protected Permission FirstPermission { get; }
		protected Permission SecondPermission { get; }

		protected Guid FirstPermissionOnly { get; }
		protected Guid SecondPermissionOnly { get; }
		protected Guid[] BothPermissions { get; }

		protected UserReadModel ReadUser => _readModel.Users.Single();

		private readonly AggregateStore<Guid> _store;
		private readonly SystemProjections _readModel;

		public UserAcceptanceBase()
		{
			_readModel = new SystemProjections();

			User = User.Create(new MagistrateUser(), new UserKey("user-01"), "Andy");
			TestRole = Role.Create(new MagistrateUser(), new RoleKey("role-01"), "Team Leader", "Leads Teams.");

			FirstPermission = Permission.Create(new MagistrateUser(), new PermissionKey("permission-01"), "First", "");
			SecondPermission = Permission.Create(new MagistrateUser(), new PermissionKey("permission-02"), "Second", "");

			FirstPermissionOnly = FirstPermission.ID;
			SecondPermissionOnly = SecondPermission.ID;
			BothPermissions = new[] { FirstPermission.ID, SecondPermission.ID };

			var es = new InMemoryEventStore();
			var wrapped = new ProjectionEventStore(es, _readModel.Project);
			_store = new AggregateStore<Guid>(wrapped);

			Project(FirstPermission);
			Project(SecondPermission);
			Project(TestRole);
			Project(User);
		}

		protected void Project(AggregateRoot<Guid> aggregateRoot)
		{
			_store.Save("TestStream", aggregateRoot);
		}
	}
}
