using System;
using System.Collections.Generic;
using System.Linq;
using Magistrate.Domain;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Domain
{
	public class PermissionInspectorTests
	{
		private readonly Permission _permission;
		private readonly Role _role;

		public PermissionInspectorTests()
		{
			var currentUser = new MagistrateUser { Name = "Test User", Key = "test-user" };
			var all = new HashSet<Permission>();
			_permission = Permission.Create(currentUser, "perm_one", "perm one", "");
			all.Add(_permission);

			Func<Guid, Permission> get = id => all.FirstOrDefault(p => p.ID == id);

			_role = Role.Create(get, currentUser, "role_one", "role one", "");
			_role.AddPermission(currentUser, _permission);

		}

		private HashSet<Role> Roles(params Role[] roles)
		{
			return new HashSet<Role>(roles);
		}

		private HashSet<Permission> Permissions(params Permission[] permissions)
		{
			return new HashSet<Permission>(permissions);
		}

		[Fact]
		public void An_included_permission_passes()
		{
			var inspector = new PermissionInspector(Roles(), Permissions(_permission), Permissions());

			inspector.Can(_permission).ShouldBe(true);
		}

		[Fact]
		public void A_permission_in_a_role_passes()
		{
			var inspector = new PermissionInspector(Roles(_role), Permissions(), Permissions());

			inspector.Can(_permission).ShouldBe(true);
		}

		[Fact]
		public void A_permission_revoked_fails()
		{
			var inspector = new PermissionInspector(Roles(), Permissions(), Permissions(_permission));

			inspector.Can(_permission).ShouldBe(false);
		}

		[Fact]
		public void A_permission_in_a_role_and_revoked_fails()
		{
			var inspector = new PermissionInspector(Roles(_role), Permissions(), Permissions(_permission));

			inspector.Can(_permission).ShouldBe(false);
		}
	}
}
