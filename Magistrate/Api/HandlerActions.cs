using System.Collections.Generic;
using Fleck;
using Ledger.Infrastructure;
using Newtonsoft.Json;

namespace Magistrate.Api
{
	public class HandlerActions
	{
		public string Json { get; }

		private readonly IEnumerable<IWebSocketConnection> _allSockets;
		private readonly IWebSocketConnection _socket;

		public HandlerActions(IEnumerable<IWebSocketConnection> allSockets, IWebSocketConnection socket, string json)
		{
			_allSockets = allSockets;
			_socket = socket;
			Json = json;
		}

		public void Reply(object message)
		{
			_socket.Send(ToJson(message));
		}

		public void Broadcast(object message)
		{
			var json = ToJson(message);
			_allSockets.ForEach(socket => socket.Send(json));
		}

		private string ToJson(object message)
		{
			return JsonConvert.SerializeObject(message, Extensions.Settings);
		}
	}
}
