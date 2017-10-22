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
			var dumpMsg = null as SC_GameBoardDump;
			yield return sendMessage<SC_GameBoardDump>(MessageType.CS_RequireGameBoard, new CS_RequireGameBoard(), MessageType.SC_GameBoardDump, (reply) => dumpMsg = reply);
			Debug.Log(dumpMsg);
		}

		private IEnumerator sendMessage<TReply>(short msgType, MessageBase msg, short replyType, Action<TReply> onReply) where TReply : MessageBase, new()
		{
			var isReplyArrived = false;
			_network.RegisterHandler(replyType, reply =>
			{
				onReply(reply.ReadMessage<TReply>());
				isReplyArrived = true;
				});
			_network.Send(msgType, msg);

			while (false == isReplyArrived)
				yield return null;

			_network.UnregisterHandler(replyType);
		}




	}
}