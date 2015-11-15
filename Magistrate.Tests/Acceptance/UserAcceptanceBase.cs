using System;
using Magistrate.Domain;

namespace Magistrate.Tests.Acceptance
{
	public class UserAcceptanceBase
	{
		protected User User { get; }
		protected Role TestRole { get; }
		protected Permission FirstPermission { get; }
		protected Permission SecondPermission { get; }

		protected Guid[] FirstPermissionOnly { get; }
		protected Guid[] SecondPermissionOnly { get; }
		protected Guid[] BothPermissions { get; }

		public UserAcceptanceBase()
		{
			User = User.Create(new MagistrateUser(), "user-01", "Andy");
			TestRole = Role.Create(new MagistrateUser(), "role-01", "Team Leader", "Leads Teams.");

			FirstPermission = Permission.Create(new MagistrateUser(), "permission-01", "First", "");
			SecondPermission = Permission.Create(new MagistrateUser(), "permission-02", "Second", "");

			FirstPermissionOnly = new[] { FirstPermission.ID };
			SecondPermissionOnly = new[] { SecondPermission.ID };
			BothPermissions = new[] { FirstPermission.ID, SecondPermission.ID };
		}
	}
}
