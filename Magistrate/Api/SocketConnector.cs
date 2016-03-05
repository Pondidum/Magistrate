using System;
using System.Collections.Generic;
using Fleck;
using Magistrate.Api.Queries;
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
		private readonly Dictionary<string, Action<Message>> _handlers;

		public SocketConnector(SystemFacade system)
		{
			_system = system;
			_server = new WebSocketServer("ws://0.0.0.0:8090");
			_sockets = new List<IWebSocketConnection>();
			_handlers = new Dictionary<string, Action<Message>>(StringComparer.OrdinalIgnoreCase);

			_handlers["USER_VALID"] = message =>
			{
				var m = (IsUsernameValidDto) message;
				Send(new IsUsernameValid(_system).Execute(m.Key));
			};
		}

		public void Configure()
		{
			_server.Start(socket =>
			{
				socket.OnOpen = () =>
				{
					_sockets.Add(socket);
					OnConnect();
				};
				socket.OnClose = () => _sockets.Remove(socket);

				socket.OnMessage = OnMessage;
			});
		}

		public void Send(object message)
		{
			var json = JsonConvert.SerializeObject(message, Extensions.Settings);
			_sockets.ForEach(socket => socket.Send(json));
		}

		private void OnMessage(string json)
		{
			var message = JsonConvert.DeserializeObject<Message>(json);

			Action<Message> handler;

			if (_handlers.TryGetValue(message.Type, out handler))
				handler(message);
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
	}

	public class Message
	{
		public string Type { get; set; }
	}

	public class IsUsernameValidDto : Message
	{
		public string Key { get; set; }
	}

	public class StateMessage
	{
		public IEnumerable<PermissionResponse> Permissions { get; set; }
		public IEnumerable<RoleResponse> Roles { get; set; }
		public IEnumerable<UserResponse> Users { get; set; }
	}
}
