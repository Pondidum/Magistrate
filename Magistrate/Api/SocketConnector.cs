using System;
using System.Collections.Generic;
using System.Linq;
using Fleck;
using Magistrate.Api.Responses;
using Magistrate.Domain.Services;
using Newtonsoft.Json;

namespace Magistrate.Api
{
	public class SocketConnector
	{
		private readonly WebSocketServer _server;
		private readonly List<IWebSocketConnection> _sockets;
		private readonly SystemFacade _system;
		private readonly Dictionary<string, Action<HandlerActions>> _handlers;

		public SocketConnector(SystemFacade system, IEnumerable<IActionHandler> handlers)
		{
			_system = system;
			_server = new WebSocketServer("ws://0.0.0.0:8090");
			_sockets = new List<IWebSocketConnection>();

			_handlers = handlers.ToDictionary(
				h => h.ActionName,
				h => new Action<HandlerActions>(h.Handle),
				StringComparer.OrdinalIgnoreCase);
		}

		public void Configure()
		{
			_server.Start(socket =>
			{
				socket.OnOpen = () => OnOpen(socket);
				socket.OnClose = () => _sockets.Remove(socket);

				socket.OnMessage = json => OnMessage(socket, json);
			});
		}

		public void Send(object message)
		{
			var sender = new HandlerActions(_sockets, null, string.Empty);
			sender.Broadcast(message);
		}

		private void OnOpen(IWebSocketConnection socket)
		{
			_sockets.Add(socket);
			OnConnect();
		}

		private void OnMessage(IWebSocketConnection socket, string json)
		{
			var message = JsonConvert.DeserializeObject<Message>(json);

			Action<HandlerActions> handler;

			if (_handlers.TryGetValue(message.Type, out handler))
				handler(new HandlerActions(_sockets, socket, json));
		}

		private void OnConnect()
		{
			Send(new StateMessage
			{
				Permissions = _system.Permissions.Map<PermissionResponse>(),
				Roles = _system.Roles.Map<RoleResponse>(),
				Users = _system.Users.Map<UserResponse>(),
			});
		}

		private class Message
		{
			public string Type { get; set; }
		}
	}

	

	public class StateMessage
	{
		public string Type { get { return "COLLECTIONS_CHANGED"; } }

		public IEnumerable<PermissionResponse> Permissions { get; set; }
		public IEnumerable<RoleResponse> Roles { get; set; }
		public IEnumerable<UserResponse> Users { get; set; }
	}
}
