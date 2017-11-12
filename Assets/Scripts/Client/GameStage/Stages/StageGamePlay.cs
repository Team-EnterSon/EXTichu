using EnterSon.Stage;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using EXTichu.Common;
using EXTichu.Common.CoreLogics;
using EnterSon.Utilities;

namespace EXTichu.Client
{
	public class StageGamePlay : Stage
	{
		private NetworkClient _network = null;

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
			GameServerManager.Instance.SendCS_JoinMatch(new CS_JoinMatch { PlayerName = "바보바보" }, null);
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
	}
}