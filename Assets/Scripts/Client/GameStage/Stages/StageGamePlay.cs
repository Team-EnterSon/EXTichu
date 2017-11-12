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

			yield return new WaitForSeconds(3.0f);

			GameServerManager.Instance.SendCS_Hello(new CS_Hello { Name = "바보" }, reply => Debug.Log(reply.Time));

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

		private IEnumerator sendMessage<TReply>(short msgType, SerializedPacket packet, short replyType, Action<TReply> onReply) where TReply : Packet<TReply>, new()
		{
			var isReplyArrived = false;
			_network.RegisterHandler(replyType, reply =>
			{
				var sourcePacket = reply.ReadMessage<SerializedPacket>();
				Debug.LogFormat("Recved msg id : <color=orange>{0}</color>, content : <color=blue>{1}</color>", msgType, sourcePacket.Source);
				onReply(Packet<TReply>.ParseFrom(sourcePacket));
				isReplyArrived = true;
				});
			_network.Send(msgType, packet);

			while (false == isReplyArrived)
				yield return null;

			_network.UnregisterHandler(replyType);
		}

	}
}