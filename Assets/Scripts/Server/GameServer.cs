using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using WSTichu.Common;

namespace WSTichu.Server
{
	public class GameServer : MonoBehaviour
	{
		NetworkServerSimple _network = null;

		public void Awake()
		{
			Action setupServerConfig = () =>
			{
				_network = new NetworkServerSimple();
			};
			Action installMessageHandlers = () =>
			{
				_network.RegisterHandler(MessageType.Connect, onConnected);
				_network.RegisterHandler(MessageType.CS_Hello, onCS_Hello);
			};

			setupServerConfig();
			installMessageHandlers();

			// TODO(sorae): Set listenPort by command line argument
			NetworkServer.Listen(8888);
		}

		private void onConnected(NetworkMessage source)
		{
			Debug.LogFormat("[{0}] New client connected : {1}", nameof(GameServer), source.conn);
		}

		private void onCS_Hello(NetworkMessage source)
		{
			var msg = source.ReadMessage<CS_Hello>();
			Debug.LogFormat("Player {0} says Hello!", msg.Name);
			source.conn.Send(MessageType.SC_World, new SC_World());
		}
	}
}