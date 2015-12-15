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

			ReadUser.Roles.Single().ID.ShouldBe(TestRole.ID);
		}

		[Fact]
		public void RemoveRole_does_nothing()
		{
			User.RemoveRole(new MagistrateUser(), TestRole);

			Project(User);

			ReadUser.Roles.ShouldBeEmpty();
		}

		[Fact]
		public void AddInclude_adds_to_the_includes_collection()
		{
			User.AddInclude(new MagistrateUser(), FirstPermission);

			Project(User);

			ReadUser.Includes.Single().ID.ShouldBe(FirstPermissionOnly);
			ReadUser.Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void AddRevoke_adds_to_the_revokes_collection()
		{
			User.AddRevoke(new MagistrateUser(), FirstPermission);

			Project(User);

			ReadUser.Revokes.Single().ID.ShouldBe(FirstPermissionOnly);
			ReadUser.Includes.ShouldBeEmpty();
		}

		[Fact]
		public void RemoveInclude_does_nothing()
		{
			User.RemoveInclude(new MagistrateUser(), FirstPermission);

			Project(User);

			ReadUser.Includes.ShouldBeEmpty();
			ReadUser.Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void RemoveRevoke_does_nothing()
		{
			User.RemoveRevoke(new MagistrateUser(), FirstPermission);

			Project(User);

			ReadUser.Includes.ShouldBeEmpty();
			ReadUser.Revokes.ShouldBeEmpty();
		}
	}
}
