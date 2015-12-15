using System.Linq;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Acceptance
{
	public class When_a_user_has_one_role_with_a_permission : UserAcceptanceBase
	{
		public When_a_user_has_one_role_with_a_permission()
		{
			TestRole.AddPermission(new MagistrateUser(), FirstPermission);
			User.AddRole(new MagistrateUser(), TestRole);
		}

		[Fact]
		public void AddInclude_a_different_permission_adds_to_the_includes_collection()
		{
			User.AddInclude(new MagistrateUser(), SecondPermission);

			Project(User);

			ReadModel.Users.Single().Includes.Single().ID.ShouldBe(SecondPermissionOnly.Single());
			ReadModel.Users.Single().Revokes.ShouldBeEmpty();
		}

		[Fact(Skip = "Not sure on whether this is the right behaviour yet")]
		public void AddInclude_the_same_permission_does_nothing()
		{
			User.AddInclude(new MagistrateUser(), FirstPermission);

			Project(User);

			ReadModel.Users.Single().Includes.ShouldBeEmpty();
			ReadModel.Users.Single().Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void RemoveInclude_a_different_permission_does_nothing()
		{
			User.RemoveInclude(new MagistrateUser(), SecondPermission);

			Project(User);

			ReadModel.Users.Single().Includes.ShouldBeEmpty();
			ReadModel.Users.Single().Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void RemoveInclude_the_same_permission_does_nothing()
		{
			User.RemoveInclude(new MagistrateUser(), FirstPermission);

			Project(User);

			ReadModel.Users.Single().Includes.ShouldBeEmpty();
			ReadModel.Users.Single().Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void AddRevoke_a_different_permission_adds_to_the_revokes_collection()
		{
			User.AddRevoke(new MagistrateUser(), SecondPermission);

			Project(User);

			ReadModel.Users.Single().Includes.ShouldBeEmpty();
			ReadModel.Users.Single().Revokes.Single().ID.ShouldBe(SecondPermissionOnly.Single());
		}

		[Fact]
		public void AddRevoke_the_same_permission_adds_to_the_revokes_collection()
		{
			User.AddRevoke(new MagistrateUser(), FirstPermission);

			Project(User);

			ReadModel.Users.Single().Includes.ShouldBeEmpty();
			ReadModel.Users.Single().Revokes.Single().ID.ShouldBe(FirstPermissionOnly.Single());
		}

		[Fact]
		public void RemoveRevoke_a_different_permission_does_nothing()
		{
			User.RemoveRevoke(new MagistrateUser(), SecondPermission);

			Project(User);

			ReadModel.Users.Single().Includes.ShouldBeEmpty();
			ReadModel.Users.Single().Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void RemoveRevoke_the_same_permission_does_nothing()
		{
			User.RemoveRevoke(new MagistrateUser(), FirstPermission);

			Project(User);

			ReadModel.Users.Single().Includes.ShouldBeEmpty();
			ReadModel.Users.Single().Revokes.ShouldBeEmpty();
		}
	}
}
