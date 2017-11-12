using EXTichu.Common;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace EXTichu.Client
{
	public partial class GameServerManager : MonoBehaviour
	{
		public static GameServerManager Instance
		{
			get
			{
				if (_cachedInstance == null)
					_cachedInstance = new GameObject(nameof(GameServerManager)).AddComponent<GameServerManager>();
				return _cachedInstance;
			}
		}
		private static GameServerManager _cachedInstance = null;

		private NetworkClient _network = new NetworkClient();

		private void installHandler<TPacket>(short packetID, Action<TPacket> handler, Func<SerializedPacket, TPacket> parser)
		{
			_network.RegisterHandler(packetID, 
				msg =>
					handler(parser(msg.ReadMessage<SerializedPacket>())));
		}

		public void Awake()
		{
			// install all handlers
			installHandler(MessageType.SC_JoinMatch, this.OnSC_JoinMatch, SC_JoinMatch.ParseFrom);
		}

		public IEnumerator ConnectToServer(string hostIP, int hostPort)
		{
			_network.Connect(hostIP, hostPort);

			while (false == _network.isConnected)
				yield return null;
			yield break;
		}

		public void SendPacket<T>(T packetToSend) where T : Packet<T>
		{
			var isTransferSucceed = _network.Send(Packet<T>.PACKET_ID, packetToSend.Serialized);
			
			if (false == isTransferSucceed)
			{
				Debug.LogError($"[{nameof(GameServerManager)}] Send packet failed! Type : {nameof(T)}");
				// TODO(sorae): Some actions like to quit app forcibly, when failed to send packet
			}
		}

		public void SendPacket<TRequest, TReply>(TRequest packetToSend, Action<TReply> onReply)
			where TRequest : Packet<TRequest> where TReply : Packet<TReply>
		{

			SendPacket(packetToSend);
			throw new NotImplementedException();
			// TODO(sorae): register temporal handler
		}
	}
}
