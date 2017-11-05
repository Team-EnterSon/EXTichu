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
			//yield return connectToServer();
			// NOTE(sorae): now network is connected

			var dummyCard = ClientCard.Factory.Create();

			var randomTable = new System.Random((int)DateTimeOffset.Now.ToUnixTimeSeconds());
			var numbers = Enum.GetValues(typeof(Card.NumberType)).Cast<Card.NumberType>();
			var shapes = Enum.GetValues(typeof(Card.ShapeType)).Cast<Card.ShapeType>();

			while (true)
			{
				yield return new WaitForSeconds(1.0f);

				dummyCard.Number = numbers.PickRandomly(randomTable);
				dummyCard.Shape  = shapes.PickRandomly(randomTable);
				dummyCard.Side = Tuple.Create(dummyCard.Shape, dummyCard.Number).IsValid()
					? Card.SideType.kFront : Card.SideType.kBack;
			}

			yield break;
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

		private IEnumerator sendMessage<TReply>(short msgType, SourcePacket packet, short replyType, Action<TReply> onReply) where TReply : Packet<TReply>, new()
		{
			var isReplyArrived = false;
			_network.RegisterHandler(replyType, reply =>
			{
				var sourcePacket = reply.ReadMessage<SourcePacket>();
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