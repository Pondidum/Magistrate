using System;
using Magistrate.Domain;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Domain
{
	public class UserTests
	{
		[Fact]
		public void A_user_must_have_a_key()
		{
			Should.Throw<ArgumentException>(
				() => User.Create("", "No key user")).Message
				.ShouldContain("Key cannot be null or whitespace");
		}

		[Fact]
		public void A_user_must_have_a_name()
		{
			Should.Throw<ArgumentException>(
				() => User.Create("some-key", "")).Message
				.ShouldContain("Name cannot be null or whitespace");
		}

		[Fact]
		public void A_user_gets_all_properties_assigned()
		{
			var user = User.Create("some-key", "some name");

			user.ShouldSatisfyAllConditions(
				() => user.ID.ShouldNotBe(Guid.Empty),
				() => user.Key.ShouldBe("some-key"),
				() => user.Name.ShouldBe("some name")
			);
		}

		[Fact]
		public void A_users_name_cannot_be_removed()
		{
			var user = User.Create("some-key", "some name");

			Should.Throw<ArgumentException>(
				() => user.ChangeName("")).Message
				.ShouldContain("Name cannot be null or whitespace");
		}

		[Fact]
		public void Changing_a_users_name_works()
		{
			var user = User.Create("some-key", "some name");

			user.ChangeName("new name");
			user.Name.ShouldBe("new name");
		}


	}
}
