using EnterSon.Stage;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using EXTichu.Common;
using EXTichu.Common.CoreLogics;
using EnterSon.Utilities;
using System.Collections.Generic;

namespace EXTichu.Client
{
	public class StageGamePlay : Stage
	{
		private NetworkClient _network = null;

		private Dictionary<PlayerPosition, Player> _players = new Dictionary<PlayerPosition, Player>
		{
			{PlayerPosition.kPlayer0, null },
			{PlayerPosition.kPlayer1, null },
			{PlayerPosition.kPlayer2, null },
			{PlayerPosition.kPlayer3, null },
		};

		public override void InitializeStage()
		{
			base.InitializeStage();

			GameServerManager.Instance.OnSC_SetReady += this.onSC_SetReady;
		}

		public override void EnterStage()
		{
			base.EnterStage();

			CardView.LoadResources();


			this.StartCoroutine(mainRoutine());
		}

		public override void ExitStage()
		{
			base.ExitStage();

			CardView.UnloadResources();
		}

		private IEnumerator mainRoutine()
		{
			yield return connectToServer();
			// Now network is connected

			yield return new WaitForSeconds(1.0f);
			GameServerManager.Instance.SendCS_JoinMatch(new CS_JoinMatch { PlayerName = DataContainer.MyNickname },
				reply => createGameBoard(reply.GameContext));
			yield break;
		}

		private IEnumerator connectToServer()
		{
			Action installHandlers = () =>
			{
			};

			installHandlers();

			yield return GameServerManager.Instance.ConnectToServer(DataContainer.HostIP, DataContainer.HostPort);
		}

		private void createGameBoard(GameDump source)
		{
			foreach(var eachPlayer in source.PlayersInGame.Values)
			{
				this._players[eachPlayer.Position] = 
			}
		}

		private void onSC_SetReady(SC_SetReady receivedPacket)
		{

		}
	}
}