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
	public class SystemTests : IDisposable
	{
		private readonly TestServer _host;

		public SystemTests()
		{
			var sys = new MagistrateSystem(new MagistrateConfiguration
			{
				EventStore = new InMemoryEventStore(),
				User = () => new Operator { Key = "user/1", Name = "Andy", CanCreatePermissions = true }
			});

			_host = TestServer.Create(sys.Configure);
		}

		public void Dispose()
		{
			_host.Dispose();
		}

		[Fact]
		public async void When_doing_a_permissions_run_through()
		{
			(await GetAll()).ShouldBeEmpty();

			await Create(new Model { Key = "perm-1", Name = "Permission 1", Description = "Permission One" });

			var permission = (await GetAll()).Single();

			permission.ShouldSatisfyAllConditions(
				() => permission.Key.ShouldBe("perm-1"),
				() => permission.Name.ShouldBe("Permission 1"),
				() => permission.Description.ShouldBe("Permission One")
			);

			await Rename(permission.Key, "First");

			(await GetSingle(permission.Key)).Name.ShouldBe("First");

			await Delete(permission.Key);

			(await GetAll()).ShouldBeEmpty();

			await Create(new Model { Key = "perm-1", Name = "Permission 1", Description = "Permission One" });
		}

		private async Task<Model> GetSingle(string key)
		{
			var response = await _host
					.HttpClient
					.GetAsync("/api/permissions/" + key);

			var json = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<Model>(json);
		}

		private async Task Rename(string key, string name)
		{
			var response = await _host
				.HttpClient
				.PutAsync("/api/permissions/" + key + "/name", AsJson(new { Name = name }));

			response.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		private async Task Delete(params string[] keys)
		{
			var response = await _host
				.HttpClient
				.SendAsync(new HttpRequestMessage(HttpMethod.Delete, "/api/permissions")
				{
					Content = AsJson(keys)
				});

			response.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		private async Task Create(Model model)
		{
			var response = await _host
				.HttpClient
				.PutAsync("/api/permissions", AsJson(model));

			response.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		private async Task<IEnumerable<Model>> GetAll()
		{
			var response = await _host
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