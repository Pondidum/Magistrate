﻿using System;
using System.Collections.Generic;
using Fleck;
using Magistrate.Api.Responses;
using Magistrate.Domain;
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
			_server = new WebSocketServer("ws://localhost:8090");
			_sockets = new List<IWebSocketConnection>();
			_handlers = new Dictionary<string, Action<Message>>(StringComparer.OrdinalIgnoreCase);
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
			var json = JsonConvert.SerializeObject(message);
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
				Roles = _system.Permissions.Map<RoleResponse>(),
				Users = _system.Permissions.Map<UserResponse>(),
			});
		}
	}

	public class Message
	{
		public string Type { get; set; }
	}

	public class StateMessage
	{
		public IEnumerable<PermissionResponse> Permissions { get; set; }
		public IEnumerable<RoleResponse> Roles { get; set; }
		public IEnumerable<UserResponse> Users { get; set; }
	}
}