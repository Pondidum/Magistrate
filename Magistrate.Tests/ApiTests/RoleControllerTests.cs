using System.Net;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.ApiTests
{
	public class RoleControllerTests : ApiAcceptanceTests
	{
		[Fact]
		public async void When_listing_all_roles()
		{
			var response = await GetJson("/api/roles/all");

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
  }
]
");

			ShouldBeTheSame(response, expected);
		}

		[Fact]
		public async void When_creating_a_role()
		{
			var response = await PutJson("/api/roles", @"
{
  ""key"":""role-new"",
  ""name"":""New Role"",
  ""description"":""A new role"",
}
");
			var expected = JToken.Parse(@"
  {
    ""key"": ""role-new"",
    ""name"": ""New Role"",
    ""description"": ""A new role"",
    ""permissions"": []
  }
");

			ShouldBeTheSame(response, expected);

		}

		[Fact]
		public async void When_adding_permission_to_a_role()
		{
			await Put("/api/roles/role-one/permissions", @"[ ""perm-two"" ]");

			var response = await GetJson("/api/roles/role-one");

			var expected = JToken.Parse(@"
{
  ""key"": ""role-one"",
  ""name"": ""first"",
  ""description"": ""first role"",
  ""permissions"": [
    {
      ""key"": ""perm-one"",
      ""name"": ""first"",
      ""description"": ""first permission""
    },
    {
      ""key"": ""perm-two"",
      ""name"": ""second"",
      ""description"": ""second permission""
    }
  ]
}
");
			ShouldBeTheSame(response, expected);
		}

		[Fact]
		public async void When_removing_a_permission_from_a_role()
		{
			await Delete("/api/roles/role-one/permissions", @"[ ""perm-one"" ]");

			var response = await GetJson("/api/roles/role-one");

			var expected = JToken.Parse(@"
{
  ""key"": ""role-one"",
  ""name"": ""first"",
  ""description"": ""first role"",
  ""permissions"": []
}
");
			ShouldBeTheSame(response, expected);
		}

		[Fact]
		public async void When_changing_a_roles_name()
		{
			await Put("/api/roles/role-one/name", @"{ ""name"": ""new name"" }");

			var response = await GetJson("/api/roles/role-one");

			var expected = JToken.Parse(@"
{
  ""key"": ""role-one"",
  ""name"": ""new name"",
  ""description"": ""first role"",
  ""permissions"": [
    {
      ""key"": ""perm-one"",
      ""name"": ""first"",
      ""description"": ""first permission""
    }
  ]
}
");
			ShouldBeTheSame(response, expected);
		}

		[Fact]
		public async void When_changing_a_roles_description()
		{
			await Put("/api/roles/role-one/description", @"{ ""description"": ""new decscription"" }");

			var response = await GetJson("/api/roles/role-one");

			var expected = JToken.Parse(@"
{
  ""key"": ""role-one"",
  ""name"": ""first"",
  ""description"": ""new description"",
  ""permissions"": [
    {
      ""key"": ""perm-one"",
      ""name"": ""first"",
      ""description"": ""first permission""
    }
  ]
}
");
		}

		[Fact]
		public async void When_deleting_a_role()
		{
			await Delete("/api/roles", @"[""role-one""]");

			var response = await Get("/api/roles/role-one");

			response.ShouldBe(HttpStatusCode.NotFound);
		}
	}
}
