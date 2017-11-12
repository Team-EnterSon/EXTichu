using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using EXTichu.Common;

namespace EXTichu.Server
{
	public class GameServer : MonoBehaviour
	{
		//-- configurations
		private int _maxGamePlayer = 4;

		private NetworkServerSimple _network = null;

		public int _nextUID { get; private set; } = 0;

		public void Awake()
		{
			Action setupServerConfig = () =>
			{
				_network = new NetworkServerSimple();
				ConnectionConfig config = new ConnectionConfig();
			};
			Action installMessageHandlers = () =>
			{
				_network.RegisterHandler(MessageType.CS_Hello, onCS_Hello);
			};

			setupServerConfig();
			installMessageHandlers();

			// TODO(sorae): Set listenPort by command line argument
			if (_network.Listen(8888))
			{
				Debug.LogFormat("Now Server is listening on port : {0}", _network.listenPort);
			}
		}

		public void Update()
		{
			_network.Update();
		}

		//-- packet handler defines
		private void onConnected(NetworkMessage source)
		{
		}

		private void onCS_Hello(NetworkMessage source)
		{
			var receivedPacket = CS_Hello.ParseFrom(source.ReadMessage<SerializedPacket>());

			source.conn.Send(MessageType.SC_World, new SC_World().Serialized);
		}

		//-- game logic functions
	}
}