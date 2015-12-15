using System;
using Ledger;
using Ledger.Stores;
using Magistrate.Domain;
using Magistrate.Domain.Services;

namespace Magistrate.Tests.Acceptance
{
	public class UserAcceptanceBase
	{
		protected User User { get; }
		protected Role TestRole { get; }
		protected Permission FirstPermission { get; }
		protected Permission SecondPermission { get; }

		protected Guid[] FirstPermissionOnly { get; }
		protected Guid[] SecondPermissionOnly { get; }
		protected Guid[] BothPermissions { get; }

		protected SystemProjections ReadModel { get; }

		private readonly AggregateStore<Guid> _store;

		public UserAcceptanceBase()
		{
			ReadModel = new SystemProjections();

			User = User.Create(new MagistrateUser(), "user-01", "Andy");
			TestRole = Role.Create(new MagistrateUser(), "role-01", "Team Leader", "Leads Teams.");

			FirstPermission = Permission.Create(new MagistrateUser(), "permission-01", "First", "");
			SecondPermission = Permission.Create(new MagistrateUser(), "permission-02", "Second", "");

			FirstPermissionOnly = new[] { FirstPermission.ID };
			SecondPermissionOnly = new[] { SecondPermission.ID };
			BothPermissions = new[] { FirstPermission.ID, SecondPermission.ID };

			var es= new InMemoryEventStore();
			var wrapped = new ProjectionEventStore(es, ReadModel.Project);
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
