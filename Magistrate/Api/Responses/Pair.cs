using Newtonsoft.Json;

namespace Magistrate.Api.Responses
{
	public class Pair
	{
		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
