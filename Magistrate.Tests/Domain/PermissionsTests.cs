using System;
using Ledger;
using Ledger.Stores;
using Magistrate.Domain;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Magistrate.Tests.Domain
{
	public class PermissionsTests
	{
		private readonly MagistrateUser _currentUser;

		public PermissionsTests()
		{
			_currentUser = new MagistrateUser
			{
				Name = "Test Current User",
				Key = "CurrentUser"
			};
		}

		[Fact]
		public void A_permission_must_have_a_key()
		{
			Should.Throw<ArgumentException>(
				() => Permission.Create(_currentUser, "", "No key permission", "doesnt have a key")).Message
				.ShouldContain("Key cannot be null or whitespace");
		}

		[Fact]
		public void A_permission_must_have_a_name()
		{
			Should.Throw<ArgumentException>(
				() => Permission.Create(_currentUser, "some-key", "", "doesnt have a name")).Message
				.ShouldContain("Name cannot be null or whitespace");
		}

		[Fact]
		public void A_permission_doesnt_need_a_description()
		{
			var permission = Permission.Create(_currentUser, "some-key", "some-name", "");

			permission.Description.ShouldBeEmpty();
		}

		[Fact]
		public void A_permission_gets_all_properties_assigned()
		{
			var permission = Permission.Create(_currentUser, "some-key", "some name", "some description");

			permission.ShouldSatisfyAllConditions(
				() => permission.ID.ShouldNotBe(Guid.Empty),
				() => permission.Key.ShouldBe("some-key"),
				() => permission.Name.ShouldBe("some name"),
				() => permission.Description.ShouldBe("some description")
			);
		}

		[Fact]
		public void A_permissions_name_cannot_be_removed()
		{
			var permission = Permission.Create(_currentUser, "some-key", "some name", "some description");

			Should.Throw<ArgumentException>(
				() => permission.ChangeName(_currentUser, "")).Message
				.ShouldContain("Name cannot be null or whitespace");
		}

		[Fact]
		public void Changing_a_permissions_name_works()
		{
			var permission = Permission.Create(_currentUser, "some-key", "some name", "some description");

			permission.ChangeName(_currentUser, "new name");
			permission.Name.ShouldBe("new name");
		}

		[Fact]
		public void Changing_a_permissions_description_works()
		{
			var permission = Permission.Create(_currentUser, "some-key", "some name", "some description");
			
			permission.ChangeDescription(_currentUser, "new description");
			permission.Description.ShouldBe("new description");
		}

		[Fact]
		public void Two_permissions_are_equal_if_they_have_the_same_id()
		{
			var p1 = Permission.Create(_currentUser, "key", "name", "");
			var p2 = Clone(p1);

			p2.ID.ShouldBe(p1.ID);
			Object.ReferenceEquals(p1, p2).ShouldBe(false);

			(p1 == p2).ShouldBe(true);
			p1.Equals(p2).ShouldBe(true);
		}

		private Permission Clone(Permission permission)
		{
			var store = new AggregateStore<Guid>(new InMemoryEventStore());
			store.Save("test", permission);

			return store.Load("test", permission.ID, () => Permission.Blank());
		}
	}
}
