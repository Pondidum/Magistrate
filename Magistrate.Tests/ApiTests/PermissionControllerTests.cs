using System;
using System.Threading.Tasks;
using Ledger;
using Ledger.Stores;
using Magistrate.Api;
using Magistrate.Domain;
using Magistrate.Domain.Events.PermissionEvents;
using Magistrate.Domain.Services;
using Microsoft.Owin.Testing;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Magistrate.Tests.ApiTests
{
	public class PermissionControllerTests : IDisposable
	{
		private readonly ITestOutputHelper _output;
		private readonly TestServer _server;

		public PermissionControllerTests(ITestOutputHelper output)
		{
			_output = output;
			var currentUser = new MagistrateUser
			{
				Key = "andy-dote",
				Name = "Andy Dote"
			};

			var p1 = Permission.Create(currentUser, new PermissionKey("perm-one"), "first", "first permission");
			var p2 = Permission.Create(currentUser, new PermissionKey("perm-two"), "second", "second permission");
			var p3 = Permission.Create(currentUser, new PermissionKey("perm-three"), "third", "third permission");

			var r1 = Role.Create(currentUser, new RoleKey("role-one"), "first", "first role");
			r1.AddPermission(currentUser, p1);

			var u1 = User.Create(currentUser, new UserKey("user-one"), "first");
			u1.AddRole(currentUser, r1);
			u1.AddInclude(currentUser, p2);
			u1.AddRevoke(currentUser, p3);

			var events = new InMemoryEventStore();
			var store = new AggregateStore<Guid>(events);
			store.Save(SystemFacade.MagistrateStream, p1);
			store.Save(SystemFacade.MagistrateStream, p2);
			store.Save(SystemFacade.MagistrateStream, p3);
			store.Save(SystemFacade.MagistrateStream, r1);
			store.Save(SystemFacade.MagistrateStream, u1);

			var config = new MagistrateConfiguration
			{
				EventStore = events,
				User = () => currentUser
			};

			var api = new MagistrateApi(config);
			_server = TestServer.Create(app =>
			{
				api.Configure(app);
			});
		}

		private async Task<JToken> Get(string url)
		{
			return JToken.Parse(await _server.HttpClient.GetStringAsync(url));
		}

		[Fact]
		public async void When_listing_all_permissions()
		{
			var response = await  Get("/api/permissions/all");

			var expected = JToken.Parse(@"
[
  {
    ""key"": ""perm-one"",
    ""name"": ""first"",
    ""description"": ""first permission""
  },
  {
    ""key"": ""perm-two"",
    ""name"": ""second"",
    ""description"": ""second permission""
  },
  {
    ""key"": ""perm-three"",
    ""name"": ""third"",
    ""description"": ""third permission""
  }
]
");

			JToken.DeepEquals(response, expected).ShouldBe(true, response.ToString);
		}

		[Fact]
		public async void When_listing_all_roles()
		{
			var response = await Get("/api/roles/all");

			var expected = JToken.Parse(@"
[
  {
    ""key"": ""role-one"",
    ""name"": ""first"",
    ""description"": ""first role"",
    ""permissions"": [
      {
        ""key"": ""perm-one"",
        ""name"": ""first"",
        ""description"": ""first permission""
      }
    ]
  }
]
");

			JToken.DeepEquals(response, expected).ShouldBe(true, response.ToString);
		}

		[Fact]
		public async void When_listing_all_users()
		{
			var response = await Get("/api/users/all");

			var expected = JToken.Parse(@"
[
  {
    ""key"": ""user-one"",
    ""name"": ""first"",
    ""isActive"": false,
    ""includes"": [
      {
        ""key"": ""perm-two"",
        ""name"": ""second"",
        ""description"": ""second permission""
      }
    ],
    ""revokes"": [
      {
        ""key"": ""perm-three"",
        ""name"": ""third"",
        ""description"": ""third permission""
      }
    ],
    ""roles"": [
      {
        ""key"": ""role-one"",
        ""name"": ""first"",
        ""description"": ""first role"",
        ""permissions"": [
          {
            ""key"": ""perm-one"",
            ""name"": ""first"",
            ""description"": ""first permission""
          }
        ]
      }
    ]
  }
]
");

			JToken.DeepEquals(response, expected).ShouldBe(true, response.ToString);
		}

		public void Dispose()
		{
			_server.Dispose();
		}
	}
}
