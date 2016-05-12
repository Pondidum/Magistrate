using System.Threading.Tasks;
using Magistrate.ReadModels;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using Owin.Routing;

namespace Magistrate.Api
{
	public class HistoryController
	{
		private readonly HistoryReadModel _history;
		private readonly JsonSerializerSettings _settings;

		public HistoryController(HistoryReadModel history, JsonSerializerSettings settings)
		{
			_history = history;
			_settings = settings;
		}

		public void Configure(IAppBuilder app)
		{
			app.Route("/api/history").Get(GetAllHistory);
		}

		private async Task GetAllHistory(IOwinContext context)
		{
			await context.WriteJson(_history.Entries, _settings);
		}
	}
}
