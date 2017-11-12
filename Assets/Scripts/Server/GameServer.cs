using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using EXTichu.Common;
using EXTichu.Common.CoreLogics;

namespace EXTichu.Server
{
	public class GameServer : MonoBehaviour
	{
		//-- configurations
		public const short MAX_PLAYERS_IN_GAME = 4;

		private NetworkServerSimple _network = null;

		private uint _nextUID = 0;

		public void Awake()
		{
			Action setupServerConfig = () =>
			{
				_network = new NetworkServerSimple();
				ConnectionConfig config = new ConnectionConfig();
			};
			Action installMessageHandlers = () =>
			{
				_network.RegisterHandler(MessageType., onCS_Hello);
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
			if(this._players.Count >= MAX_PLAYERS_IN_GAME)
			{
				// room is full
				Debug.Log($"Kicked user {source.conn.address} because room is full");
				source.conn.Disconnect();
			}

			var newPlayer = new ServerPlayer();
			newPlayer.Name

		}

		private void onCS_SetReady(NetworkMessage source)
		{

		}

		#region game logics
		private List<Player> _players = new List<Player>();
		#endregion
	}
}