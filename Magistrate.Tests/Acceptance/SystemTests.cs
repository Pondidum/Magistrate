using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ledger.Stores;
using Microsoft.Owin.Testing;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Acceptance
{
	public class SystemTests
	{
		[Fact]
		public async void When_doing_a_permissions_run_through()
		{
			var sys = new MagistrateSystem(new MagistrateConfiguration
			{
				EventStore = new InMemoryEventStore(),
				User = () => new Operator { Key = "user/1", Name = "Andy", CanCreatePermissions = true }
			});

			using (var host = TestServer.Create(sys.Configure))
			{
				(await GetAll(host)).ShouldBeEmpty();

				await Create(host, new Model { Key = "perm-1", Name = "Permission 1", Description = "Permission One" });

				var permission = (await GetAll(host)).Single();

				permission.ShouldSatisfyAllConditions(
					() => permission.Key.ShouldBe("perm-1"),
					() => permission.Name.ShouldBe("Permission 1"),
					() => permission.Description.ShouldBe("Permission One")
				);

				await Rename(host, permission.Key, "First");
				
				(await GetSingle(host, permission.Key)).Name.ShouldBe("First");

				await Delete(host, permission.Key);

				(await GetAll(host)).ShouldBeEmpty();

				await Create(host, new Model { Key = "perm-1", Name = "Permission 1", Description = "Permission One" });
			}
		}

		private static async Task<Model> GetSingle(TestServer host, string key)
		{
			var response = await host
					.HttpClient
					.GetAsync("/api/permissions/" + key);

			var json = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<Model>(json);
		}

		private static async Task Rename(TestServer host, string key, string name)
		{
			var response = await host
				.HttpClient
				.PutAsync("/api/permissions/" + key + "/name", AsJson(new { Name = name }));

			response.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		private static async Task Delete(TestServer host, params string[] keys)
		{
			var response = await host
				.HttpClient
				.SendAsync(new HttpRequestMessage(HttpMethod.Delete, "/api/permissions")
				{
					Content = AsJson(keys)
				});

			response.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		private static async Task Create(TestServer host, Model model)
		{
			var response = await host
				.HttpClient
				.PutAsync("/api/permissions", AsJson(model));

			response.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		private static async Task<IEnumerable<Model>> GetAll(TestServer host)
		{
			var response = await host
					.HttpClient
					.GetAsync("/api/permissions");

			var json = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<IEnumerable<Model>>(json);
		}

		private static StringContent AsJson(object content)
		{
			var json = JsonConvert.SerializeObject(content);
			return new StringContent(json, Encoding.UTF8, "application/json");
		}

		private class Model
		{
			public string Key { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
		}
	}
}