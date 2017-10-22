using EnterSon.Stage;
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

			StartCoroutine(connectToServerRoutine());
		}

		private IEnumerator connectToServerRoutine()
		{
			_network = new NetworkClient();
			_network.Connect(DataContainer.HostIP, DataContainer.HostPort);
			_network.RegisterHandler(MessageType.SC_World, (_) => Debug.Log("Server says World!"));

			while (_network.isConnected == false)
			{
				yield return null;
			}
			_network.Send(MessageType.CS_Hello, new CS_Hello { Name = "Sorae" });
		}


	}
}