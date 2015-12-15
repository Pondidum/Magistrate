using System.Linq;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Acceptance
{
	public class When_a_user_has_no_permissions_and_no_roles : UserAcceptanceBase
	{
		[Fact]
		public void AddRole_adds_a_role()
		{
			User.AddRole(new MagistrateUser(), TestRole);

			Project(User);

			ReadModel.Users.Single().Roles.Single().ID.ShouldBe(TestRole.ID);
		}

		[Fact]
		public void RemoveRole_does_nothing()
		{
			User.RemoveRole(new MagistrateUser(), TestRole);

			Project(User);

			ReadModel.Users.Single().Roles.ShouldBeEmpty();
		}

		[Fact]
		public void AddInclude_adds_to_the_includes_collection()
		{
			User.AddInclude(new MagistrateUser(), FirstPermission);

			Project(User);

			ReadModel.Users.Single().Includes.Single().ID.ShouldBe(FirstPermissionOnly.Single());
			ReadModel.Users.Single().Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void AddRevoke_adds_to_the_revokes_collection()
		{
			User.AddRevoke(new MagistrateUser(), FirstPermission);

			Project(User);

			ReadModel.Users.Single().Revokes.Single().ID.ShouldBe(FirstPermissionOnly.Single());
			ReadModel.Users.Single().Includes.ShouldBeEmpty();
		}

		[Fact]
		public void RemoveInclude_does_nothing()
		{
			User.RemoveInclude(new MagistrateUser(), FirstPermission);

			Project(User);

			ReadModel.Users.Single().Includes.ShouldBeEmpty();
			ReadModel.Users.Single().Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void RemoveRevoke_does_nothing()
		{
			User.RemoveRevoke(new MagistrateUser(), FirstPermission);

			Project(User);

			ReadModel.Users.Single().Includes.ShouldBeEmpty();
			ReadModel.Users.Single().Revokes.ShouldBeEmpty();
		}
	}
}
