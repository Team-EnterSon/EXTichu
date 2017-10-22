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
		private NetworkServerSimple _network = null;
		private ConnectionConfig _config = null;

		public void Awake()
		{
			Action setupServerConfig = () =>
			{
				_network = new NetworkServerSimple();
				_config = new ConnectionConfig();
			};
			Action installMessageHandlers = () =>
			{
				_network.RegisterHandler(MessageType.Connect, onConnected);
				_network.RegisterHandler(MessageType.CS_RequireGameBoard, onCS_RequireGameBoard)
			};

			setupServerConfig();
			installMessageHandlers();
			
			// TODO(sorae): Set listenPort by command line argument
			if(_network.Listen(8888))
			{
				Debug.LogFormat("Now Server is listening on port : {0}", _network.listenPort);
			}
		}

		private void onCS_RequireGameBoard(NetworkMessage netMsg)
		{
			var message = CS_RequireGameBoard.ParseFrom(netMsg);

		}

		public void Update()
		{
			_network.Update();
		}

		private void onConnected(NetworkMessage source)
		{
			Debug.LogFormat("[{0}] New client connected : {1}", nameof(GameServer), source.conn);
		}


	}
}