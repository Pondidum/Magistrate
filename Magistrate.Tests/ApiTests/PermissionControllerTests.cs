﻿using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.ApiTests
{
	public class PermissionControllerTests : ApiAcceptanceTests
	{
		[Fact]
		public async void When_listing_all_permissions()
		{
			var response = await GetJson("/api/permissions");

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

			ShouldBeTheSame(response, expected);
		}

		[Fact]
		public async void When_creating_a_permission()
		{
			var response = await Put("/api/permissions", @"
{
  ""key"":""perm-new"",
  ""name"":""New Permission"",
  ""description"":""A new permission"",
}
");
			var expected = JToken.Parse(@"
  {
    ""key"": ""perm-new"",
    ""name"": ""New Permission"",
    ""description"": ""A new permission""
  }
");

			response.ShouldBe(HttpStatusCode.OK);
		}

		[Fact]
		public async void When_changing_a_permissions_name()
		{
			await Put("/api/permissions/perm-one/name", @"{ ""name"":""replaced name"" }");
			var response = await GetJson("/api/permissions/perm-one");

			var expected = JToken.Parse(@"
  {
    ""key"": ""perm-one"",
    ""name"": ""replaced name"",
    ""description"": ""first permission""
  }
");
			ShouldBeTheSame(response, expected);
		}

		[Fact]
		public async void When_changing_a_permissions_description()
		{
			await Put("/api/permissions/perm-one/description", @"{ ""description"":""replaced description"" }");
			var response = await GetJson("/api/permissions/perm-one");

			var expected = JToken.Parse(@"
{
  ""key"": ""perm-one"",
  ""name"": ""first"",
  ""description"": ""replaced description""
}
");
			ShouldBeTheSame(response, expected);
		}

		[Fact]
		public async void When_deleteing_a_permission()
		{
			await Delete("/api/permissions", @"[""perm-one""]");

			var response = await Get("/api/permissions/perm-one");

			response.ShouldBe(HttpStatusCode.NotFound);
		}
	}
}
