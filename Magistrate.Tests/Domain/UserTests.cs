using System;
using System.Linq;
using Ledger.Infrastructure;
using Magistrate.Domain;
using Magistrate.Domain.Events.UserEvents;
using Magistrate.Domain.Services;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Domain
{
	public class UserTests
	{
		private readonly User _user;
		private Operator _cu;
		private Permission _permissionOne;
		private Permission _permissionTwo;

		public UserTests()
		{
			_cu = new Operator
			{
				CanCreatePermissions = true,
				CanCreateRoles = true,
				CanCreateUsers = true
			};

			_user = User.Blank();
			var ps = new PermissionService();

			_permissionOne = Permission.Create(ps, _cu, new PermissionKey("perm-one"), "One", "");
			_permissionTwo = Permission.Create(ps, _cu, new PermissionKey("perm-two"), "Two", "");
		}

		private void EventsShouldBe(params Type[] events)
		{
			_user.GetUncommittedEvents().Select(e => e.GetType()).ShouldBe(events);
		}

		[Fact]
		public void When_the_user_has_no_permissions()
		{
			_user.ShouldSatisfyAllConditions(
				() => _user.Includes.ShouldBeEmpty(),
				() => _user.Revokes.ShouldBeEmpty(),
				() => _user.Roles.ShouldBeEmpty()
			);

			_user.GetUncommittedEvents().ShouldBeEmpty();
		}

		[Fact]
		public void When_an_include_is_added()
		{
			_user.AddInclude(_cu, _permissionOne);

			_user.ShouldSatisfyAllConditions(
				() => _user.Includes.ShouldBe(new[] { _permissionOne.ID }),
				() => _user.Revokes.ShouldBeEmpty(),
				() => _user.Roles.ShouldBeEmpty()
			);

			EventsShouldBe(
				typeof(IncludeAddedToUserEvent));
		}

		[Fact]
		public void When_a_revoke_is_added()
		{
			_user.AddRevoke(_cu, _permissionOne);

			_user.ShouldSatisfyAllConditions(
				() => _user.Includes.ShouldBeEmpty(),
				() => _user.Revokes.ShouldBe(new[] { _permissionOne.ID }),
				() => _user.Roles.ShouldBeEmpty()
			);

			EventsShouldBe(
				typeof(RevokeAddedToUserEvent));
		}

		[Fact]
		public void When_the_user_has_a_revoke_and_adds_an_include()
		{
			_user.AddRevoke(_cu, _permissionOne);
			_user.AddInclude(_cu, _permissionOne);

			_user.ShouldSatisfyAllConditions(
				() => _user.Includes.ShouldBe(new[] { _permissionOne.ID }),
				() => _user.Revokes.ShouldBeEmpty(),
				() => _user.Roles.ShouldBeEmpty()
			);

			EventsShouldBe(
				typeof(RevokeAddedToUserEvent), 
				typeof(RevokeRemovedFromUserEvent), 
				typeof(IncludeAddedToUserEvent));
		}

		[Fact]
		public void When_a_role_is_added()
		{
			var role = Role.Create(new RoleService(), _cu, new RoleKey("role-one"), "role one", "");
			role.AddPermission(_cu, _permissionOne.ID);

			_user.AddRole(_cu, role);

			_user.ShouldSatisfyAllConditions(
				() => _user.Includes.ShouldBeEmpty(),
				() => _user.Revokes.ShouldBeEmpty(),
				() => _user.Roles.ShouldBe(new[] { role.ID })
			);

			EventsShouldBe(
				typeof(RoleAddedToUserEvent));
		}

		[Fact]
		public void When_the_user_has_a_revoked_permission_included_by_a_role()
		{
			var role = Role.Create(new RoleService(), _cu, new RoleKey("role-one"), "role one", "");
			role.AddPermission(_cu, _permissionOne.ID);

			_user.AddRole(_cu, role);
			_user.AddRevoke(_cu, _permissionOne);

			_user.ShouldSatisfyAllConditions(
				() => _user.Includes.ShouldBeEmpty(),
				() => _user.Revokes.ShouldBe(new[] { _permissionOne.ID }),
				() => _user.Roles.ShouldBe(new[] { role.ID })
			);

			EventsShouldBe(
				typeof(RoleAddedToUserEvent),
				typeof(RevokeAddedToUserEvent));
		}

		[Fact]
		public void When_adding_the_same_include_twice()
		{
			_user.AddInclude(_cu, _permissionOne);
			_user.AddInclude(_cu, _permissionOne);

			EventsShouldBe(
				typeof(IncludeAddedToUserEvent));
		}

		[Fact]
		public void When_adding_the_same_revoke_twice()
		{
			_user.AddRevoke(_cu, _permissionOne);
			_user.AddRevoke(_cu, _permissionOne);

			EventsShouldBe(
				typeof(RevokeAddedToUserEvent));
		}

		[Fact]
		public void When_removing_a_non_existing_include()
		{
			_user.RemoveInclude(_cu, _permissionOne);

			EventsShouldBe();
		}

		[Fact]
		public void When_removing_a_non_existing_revoke()
		{
			_user.RemoveRevoke(_cu, _permissionOne);

			EventsShouldBe();
		}

		[Fact]
		public void When_the_user_has_a_revoke_and_it_is_added_as_an_include()
		{
			_user.AddRevoke(_cu, _permissionOne);
			_user.AddInclude(_cu, _permissionOne);

			EventsShouldBe(
				typeof(RevokeAddedToUserEvent),
				typeof(RevokeRemovedFromUserEvent),
				typeof(IncludeAddedToUserEvent));
		}

		[Fact]
		public void When_the_user_has_an_include_and_it_is_added_as_a_revoke()
		{
			_user.AddInclude(_cu, _permissionOne);
			_user.AddRevoke(_cu, _permissionOne);

			EventsShouldBe(
				typeof(IncludeAddedToUserEvent),
				typeof(IncludeRemovedFromUserEvent),
				typeof(RevokeAddedToUserEvent));
		}

		[Fact]
		public void Multiple_roles_with_the_same_key_cannot_be_created()
		{
			var service = new UserService();

			var u1 = User.Create(service, _cu, new UserKey("1"), "name");

			//simulate saving to the eventstore
			u1.GetUncommittedEvents().ForEach(service.Project);

			Should.Throw<ArgumentException>(() => User.Create(service, _cu, new UserKey("1"), "new"));
		}
	}
}
