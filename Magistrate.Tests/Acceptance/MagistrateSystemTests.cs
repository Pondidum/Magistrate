using System;
using Ledger;
using Ledger.Stores;
using Magistrate.Domain;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Acceptance
{
	public class MagistrateSystemTests
	{
		private static readonly MagistrateUser CurrentUser = new MagistrateUser();

		[Fact]
		public void When_adding_and_removing_permission()
		{
			var store = new AggregateStore<Guid>(new InMemoryEventStore<Guid>());
			var system = new MagistrateSystem(store);

			var permission = Permission.Create(CurrentUser, "first", "First Permission", "");

			system.AddPermission(permission);
			system.Permissions.ShouldContain(p => p.Key == "first");

			system.RemovePermission(permission);
			system.Permissions.ShouldBeEmpty();
		}
	}
}
