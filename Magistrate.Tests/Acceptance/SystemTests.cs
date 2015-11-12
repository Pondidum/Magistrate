using System;
using Ledger;
using Ledger.Stores;
using Magistrate.Domain;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Acceptance
{
	public class SystemTests
	{
		private readonly Store _store;
		private readonly MagistrateUser _currentUser;
		private User _user;
		private Role _cleaner;

		public SystemTests()
		{
			var aggregateStore = new InMemoryEventStore<Guid>();
			_store = new Store(new AggregateStore<Guid>(aggregateStore));
			_currentUser = new MagistrateUser { Key = "MrUser", Name = "User" };

			_store.Save(Permission.Create(_currentUser, "create-person", "Create Person", ""));
			_store.Save(Permission.Create(_currentUser, "archive-person", "Archive Person", ""));
			_store.Save(Permission.Create(_currentUser, "restore-person", "Restore Person", ""));
			_store.Save(Permission.Create(_currentUser, "delete-person", "Delete Person", ""));

			_cleaner = Role.Create(_store.Permissions.ByID, _currentUser, "cleaner", "Database Cleaner", "");
			_cleaner.AddPermission(_currentUser, _store.Permissions.ByKey("archive-person"));
			_cleaner.AddPermission(_currentUser, _store.Permissions.ByKey("restore-person"));
			_store.Save(_cleaner);

			var admin = Role.Create(_store.Permissions.ByID, _currentUser, "admin", "Database Admin", "");
			admin.AddPermission(_currentUser, _store.Permissions.ByKey("archive-person"));
			admin.AddPermission(_currentUser, _store.Permissions.ByKey("restore-person"));
			admin.AddPermission(_currentUser, _store.Permissions.ByKey("delete-person"));
			admin.AddPermission(_currentUser, _store.Permissions.ByKey("create-person"));
			_store.Save(admin);


			_user = User.Create(_store.Permissions.ByID, _store.Roles.ByID, _currentUser, "users/001", "user one");
			_store.Save(_user);
		}

		[Fact]
		public void When_a_user_has_a_role_and_it_has_a_permission_added()
		{
			_user.AddRole(_currentUser, _cleaner);
			_store.Save(_user);

			_cleaner.AddPermission(_currentUser, _store.Permissions.ByKey("delete-person"));
			_store.Save(_cleaner);

			_user.Permissions.All.ShouldContain(p => p.Key == "delete-person");
		}

		[Fact]
		public void When_a_user_has_a_role_and_it_has_a_permission_removed()
		{
			_user.AddRole(_currentUser, _cleaner);
			_store.Save(_user);

			_cleaner.RemovePermission(_currentUser, _store.Permissions.ByKey("archive-person"));
			_store.Save(_cleaner);

			_user.Permissions.All.ShouldNotContain(p => p.Key == "archive-person");
		}

		[Fact]
		public void When_a_user_has_a_permission_which_gets_deleted()
		{
			_user.AddPermission(_currentUser, _store.Permissions.ByKey("restore-person"));
			_store.Save(_user);


			var perm = _store.Permissions.ByKey("restore-person");
			perm.Delete(_currentUser);
			_store.Save(perm);

			_user.Permissions.All.ShouldNotContain(p => p.Key == "restore-person");
		}
	}
}
