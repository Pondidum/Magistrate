using System;
using Magistrate.Domain;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Domain
{
	public class PermissionsTests
	{
		[Fact]
		public void A_permission_must_have_a_key()
		{
			Should.Throw<ArgumentException>(
				() => Permission.Create("", "No key permission", "doesnt have a key")).Message
				.ShouldContain("Key cannot be null or whitespace");
		}

		[Fact]
		public void A_permission_must_have_a_name()
		{
			Should.Throw<ArgumentException>(
				() => Permission.Create("some-key", "", "doesnt have a name")).Message
				.ShouldContain("Name cannot be null or whitespace");
		}

		[Fact]
		public void A_permission_doesnt_need_a_description()
		{
			var permission = Permission.Create("some-key", "some-name", "");

			permission.Description.ShouldBeEmpty();
		}

		[Fact]
		public void A_permission_gets_all_properties_assigned()
		{
			var permission = Permission.Create("some-key", "some name", "some description");

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
			var permission = Permission.Create("some-key", "some name", "some description");

			Should.Throw<ArgumentException>(
				() => permission.ChangeName("")).Message
				.ShouldContain("Name cannot be null or whitespace");
		}

		[Fact]
		public void Changing_a_permissions_name_works()
		{
			var permission = Permission.Create("some-key", "some name", "some description");

			permission.ChangeName("new name");
			permission.Name.ShouldBe("new name");
		}

		[Fact]
		public void Changing_a_permissions_description_works()
		{
			var permission = Permission.Create("some-key", "some name", "some description");
			
			permission.ChangeDescription("new description");
			permission.Description.ShouldBe("new description");
		}
	}
}
