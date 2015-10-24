using System;
using Magistrate.Domain;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Domain
{
	public class RolesTests
	{
		[Fact]
		public void A_role_must_have_a_key()
		{
			Should.Throw<ArgumentException>(
				() => Role.Create("", "No key role", "doesnt have a key")).Message
				.ShouldContain("Key cannot be null or whitespace");
		}

		[Fact]
		public void A_role_must_have_a_name()
		{
			Should.Throw<ArgumentException>(
				() => Role.Create("some-key", "", "doesnt have a name")).Message
				.ShouldContain("Name cannot be null or whitespace");
		}

		[Fact]
		public void A_role_doesnt_need_a_description()
		{
			var role = Role.Create("some-key", "some-name", "");

			role.Description.ShouldBeEmpty();
		}

		[Fact]
		public void A_role_gets_all_properties_assigned()
		{
			var role = Role.Create("some-key", "some name", "some description");

			role.ShouldSatisfyAllConditions(
				() => role.ID.ShouldNotBe(Guid.Empty),
				() => role.Key.ShouldBe("some-key"),
				() => role.Name.ShouldBe("some name"),
				() => role.Description.ShouldBe("some description")
			);
		}

		[Fact]
		public void A_roles_name_cannot_be_removed()
		{
			var role = Role.Create("some-key", "some name", "some description");

			Should.Throw<ArgumentException>(
				() => role.ChangeName("")).Message
				.ShouldContain("Name cannot be null or whitespace");
		}

		[Fact]
		public void Changing_a_roles_name_works()
		{
			var role = Role.Create("some-key", "some name", "some description");

			role.ChangeName("new name");
			role.Name.ShouldBe("new name");
		}

		[Fact]
		public void Changing_a_roles_description_works()
		{
			var role = Role.Create("some-key", "some name", "some description");

			role.ChangeDescription("new description");
			role.Description.ShouldBe("new description");
		}
	}
}
