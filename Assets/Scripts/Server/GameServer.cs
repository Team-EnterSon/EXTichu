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
				_network.RegisterHandler(MessageType.CS_JoinMatch, onCS_JoinMatch);
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
		}

		private void onCS_JoinMatch(NetworkMessage source)
		{
			if(this._players.Count >= MAX_PLAYERS_IN_GAME)
			{
				// room is full
				Debug.Log($"Kicked user {source.conn.address} because room is full");
				source.conn.Disconnect();
			}

			var receivedPacket = CS_JoinMatch.ParseFrom(source);

			var newPlayerPosition = this._players.Where(kvp => kvp.Value == null).First().Key;

			var newPlayer = new ServerPlayer();
			newPlayer.Name = receivedPacket.PlayerName;
			newPlayer.UID = _nextUID++;
			newPlayer.Position = newPlayerPosition;
			newPlayer.Team = 
				newPlayerPosition == PlayerPosition.kPlayer0 || newPlayerPosition == PlayerPosition.kPlayer2
				? TeamType.kTeam0 : TeamType.kTeam1;

			this._players[newPlayer.Position] = newPlayer;


			var response = new SC_JoinMatch();
			response.GameContext.PlayersInGame = _players;

			source.conn.Send(MessageType.SC_JoinMatch, response.Serialized);
		}

		private void onCS_SetReady(NetworkMessage source)
		{

		}

		#region game logics
		private Dictionary<PlayerPosition, Player> _players = new Dictionary<PlayerPosition, Player>()
		{
			{PlayerPosition.kPlayer0, null },
			{PlayerPosition.kPlayer1, null },
			{PlayerPosition.kPlayer2, null },
			{PlayerPosition.kPlayer3, null },
		};
		#endregion
	}
}