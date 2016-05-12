using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Magistrate.Tests.ApiTests
{
	public class HistoryControllerTests : ApiAcceptanceTests
	{
		private readonly ITestOutputHelper _output;

		public HistoryControllerTests(ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact]
		public async void When_listing_all_history()
		{
			var response = await GetJson("/api/history");


			_output.WriteLine(response.ToString());
		}
	}
}
