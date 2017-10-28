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
		//-- configurations
		private int _maxGamePlayer = 4;

		private NetworkServerSimple _network = null;
		private GameBoard _gameBoard = null;

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
				_network.RegisterHandler(MessageType.Connect, onConnected);
				_network.RegisterHandler(MessageType.CS_RequestGameBoard, onRequestGameBoard);
			};
			Action createGameBoard = () =>
			{
				_gameBoard = new GameBoard();
				
				for(TeamType team=TeamType.kTeamBegin; team < TeamType.kTeamEnd; ++team)
				{
					_gameBoard.Players.Add(team, new List<Player>());
				}
			};

			setupServerConfig();
			installMessageHandlers();
			createGameBoard();

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
			if(!AddPlayerToGameBaord())
			{
				// failed to add board.
				return;
			}

			TeamType myTeam = _gameBoard.GetAvailableTeam();
			Player player = new Player { UID = _nextUID++, Team = myTeam };

			_gameBoard.Players[myTeam].Add(player);

			// send message back to let the client to know its UID
			SC_ConnectSuccess msg = new SC_ConnectSuccess();
			msg.player = player;
			source.conn.Send(MessageType.SC_ConnectSuccess, msg.ToPacket());

			Debug.LogFormat("[{0}] New client connected : {1}", nameof(GameServer), source.conn);
		}

		private void onRequestGameBoard(NetworkMessage netMsg)
		{

		}

		//-- game logic functions
		private bool AddPlayerToGameBaord()
		{
			if (_gameBoard.Players.Count >= _maxGamePlayer)
				return false;

			return true;
		}
	}
}