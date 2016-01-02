using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.ApiTests
{
	public class UserControllerTests : ApiAcceptanceTests
	{
		[Fact]
		public async void When_listing_all_users()
		{
			var response = await GetJson("/api/users/all");

			var expected = JToken.Parse(@"
[
  {
    ""key"": ""user-one"",
    ""name"": ""first"",
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

			ShouldBeTheSame(response, expected);
		}

		[Fact]
		public async void When_creating_a_user()
		{
			var response = await PutJson("/api/users", @"
{
  ""key"":""user-two"",
  ""name"": ""second"",
}
");

			var expected = JToken.Parse(@"
{
  ""key"":""user-two"",
  ""name"": ""second"",
  ""includes"": [],
  ""revokes"": [],
  ""roles"": []
}");


			ShouldBeTheSame(response, expected);
		}

		[Fact]
		public async void When_changing_a_users_name()
		{
			await Put("/api/users/user-one/name", @"{ ""name"": ""new name""}");
			var response = await GetJson("/api/users/user-one");

			response.SelectToken("name").Value<string>().ShouldBe("new name");
		}

		[Fact]
		public async void When_adding_an_include()
		{
			await Put("/api/users/user-one/includes", @"[ ""perm-one"" ]");
			var response = await GetJson("/api/users/user-one");

			var expected = JToken.Parse(@"
[
  {
    ""key"": ""perm-two"",
    ""name"": ""second"",
    ""description"": ""second permission""
  },
  {
    ""key"": ""perm-one"",
    ""name"": ""first"",
    ""description"": ""first permission""
  },
]");

			ShouldBeTheSame(response.SelectToken("includes"), expected);
		}

		[Fact]
		public async void When_removing_an_include()
		{
			await Delete("/api/users/user-one/includes", @"[ ""perm-two"" ]");
			var response = await GetJson("/api/users/user-one");

			var expected = JToken.Parse(@"[]");

			ShouldBeTheSame(response.SelectToken("includes"), expected);
		}

		[Fact]
		public async void When_adding_a_revoke()
		{
			await Put("/api/users/user-one/revokes", @"[ ""perm-one"" ]");
			var response = await GetJson("/api/users/user-one");

			var expected = JToken.Parse(@"
[
  {
    ""key"": ""perm-three"",
    ""name"": ""third"",
    ""description"": ""third permission""
  },
  {
    ""key"": ""perm-one"",
    ""name"": ""first"",
    ""description"": ""first permission""
  },
]");

			ShouldBeTheSame(response.SelectToken("revokes"), expected);
		}

		[Fact]
		public async void When_removing_a_revoke()
		{
			await Delete("/api/users/user-one/revokes", @"[ ""perm-three"" ]");
			var response = await GetJson("/api/users/user-one");

			var expected = JToken.Parse(@"[]");

			ShouldBeTheSame(response.SelectToken("revokes"), expected);
		}

		[Fact]
		public async void When_adding_a_role()
		{
			await Put("/api/users/user-one/roles", @"[ ""role-two"" ]");
			var response = await GetJson("/api/users/user-one");

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
  },
  {
    ""key"": ""role-two"",
    ""name"": ""second"",
    ""description"": ""second role"",
    ""permissions"": [
        {
        ""key"": ""perm-two"",
        ""name"": ""second"",
        ""description"": ""second permission""
        }
    ]
  },
]");
			ShouldBeTheSame(response.SelectToken("roles"), expected);
		}

		[Fact]
		public async void When_removing_a_role()
		{
			await Delete("/api/users/user-one/roles", @"[ ""role-one"" ]");
			var response = await GetJson("/api/users/user-one");

			var expected = JToken.Parse("[]");

			ShouldBeTheSame(response.SelectToken("roles"), expected);
		}

		[Fact]
		public async void When_removing_a_user()
		{
			await Delete("/api/users", @"[""user-one""]");

			var response = await Get("/api/users/user-one");

			response.ShouldBe(HttpStatusCode.NotFound);
		}

		[Fact]
		public async void When_getting_history()
		{
			var response = await GetJson("/api/users/user-one/history");
			var entry = response.First();

			entry.ShouldSatisfyAllConditions(
				() => entry.SelectToken("action").Value<string>().ShouldBe("UserCreatedEvent"),
				() => entry.SelectToken("onAggregate").ShouldBe(null),
				() => entry.SelectToken("at").Value<DateTime>().ShouldBeGreaterThan(DateTime.MinValue),
				() => ShouldBeTheSame(entry.SelectToken("by"), JToken.Parse(@" { ""name"": ""Andy Dote"", ""key"": ""andy-dote"" }"))
			);

			response
				.Select(t => t.SelectToken("action")
				.Value<string>())
				.ShouldBe(new[] { "UserCreatedEvent", "RoleAddedToUserEvent", "IncludeAddedToUserEvent", "RevokeAddedToUserEvent" });
		}
	}
}