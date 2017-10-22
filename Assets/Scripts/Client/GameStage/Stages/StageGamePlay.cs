using EnterSon.Stage;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using WSTichu.Common;

namespace WSTichu.Client
{
	public class StageGamePlay : Stage
	{
		private NetworkClient _network = null;

		public override void EnterStage()
		{
			base.EnterStage();



			this.StartCoroutine(mainRoutine());
		}

		private IEnumerator mainRoutine()
		{
			yield return connectToServer();
			// NOTE(sorae): now network is connected


		}

		private IEnumerator connectToServer()
		{
			Action installHandlers = () =>
			{
				_network.RegisterHandler(MessageType.SC_World, (_) => Debug.Log("Server says World!"));
			};

			_network = new NetworkClient();

			installHandlers();
			_network.Connect(DataContainer.HostIP, DataContainer.HostPort);

			while (_network.isConnected == false)
			{
				yield return null;
			}
			yield break;
		}

		private IEnumerator constructGameBoard()
		{
			_network.Send()
		}




	}
}