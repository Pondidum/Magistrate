using System;
using Magistrate.Domain;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Acceptance
{
	public class When_a_user_has_no_permissions_and_no_roles
	{
		private User User { get; }
		private Role TestRole { get; }
		private Permission FirstPermission { get; }
		private Permission SecondPermission { get; }

		private Guid[] FirstPermissionOnly { get; }
		private Guid[] SecondPermissionOnly { get; }
		private Guid[] BothPermissions { get; }

		public When_a_user_has_no_permissions_and_no_roles()
		{
			User = User.Create(new MagistrateUser(), "user-01", "Andy");
			TestRole = Role.Create(new MagistrateUser(), "role-01", "Team Leader", "Leads Teams.");

			FirstPermission = Permission.Create(new MagistrateUser(), "permission-01", "First", "");
			SecondPermission = Permission.Create(new MagistrateUser(), "permission-02", "Second", "");

			FirstPermissionOnly = new[] { FirstPermission.ID };
			SecondPermissionOnly = new[] { SecondPermission.ID };
			BothPermissions = new[] { FirstPermission.ID, SecondPermission.ID };
		}



		[Fact]
		public void AddRole_adds_a_role()
		{
			User.AddRole(new MagistrateUser(), TestRole);

			User.Roles.ShouldBe(new[] { TestRole.ID });
		}

		[Fact]
		public void RemoveRole_does_nothing()
		{
			User.RemoveRole(new MagistrateUser(), TestRole);

			User.Roles.ShouldBeEmpty();
		}

		[Fact]
		public void AddInclude_adds_to_the_includes_collection()
		{
			User.AddInclude(new MagistrateUser(), FirstPermission);

			User.Includes.ShouldBe(FirstPermissionOnly);
			User.Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void AddRevoke_adds_to_the_revokes_collection()
		{
			User.AddRevoke(new MagistrateUser(), FirstPermission);

			User.Revokes.ShouldBe(FirstPermissionOnly);
			User.Includes.ShouldBeEmpty();
		}

		[Fact]
		public void RemoveInclude_does_nothing()
		{
			User.RemoveInclude(new MagistrateUser(), FirstPermission);

			User.Includes.ShouldBeEmpty();
			User.Revokes.ShouldBeEmpty();
		}

		[Fact]
		public void RemoveRevoke_does_nothing()
		{
			User.RemoveRevoke(new MagistrateUser(), FirstPermission);

			User.Includes.ShouldBeEmpty();
			User.Revokes.ShouldBeEmpty();
		}
	}
}
