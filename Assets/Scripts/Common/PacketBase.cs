using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace EXTichu.Common
{
	public class SerializedPacket : MessageBase
	{
		public string Source = default(string);
	}

	public abstract class PacketBase { }

	public abstract class Packet<T> : PacketBase where T : Packet<T>
	{
		public static readonly short PACKET_ID = typeof(T).GetCustomAttributes(true)
			.Where(eachAttr => eachAttr is PacketWithID).Cast<PacketWithID>()
			.FirstOrDefault().PacketID;

		public static T ParseFrom(SerializedPacket source)
		{
#if DEBUG
			Debug.Log($"[PacketDump] Type : {nameof(T)}, content : {source.Source}");
#endif
			return JsonConvert.DeserializeObject<T>(source.Source);
		}

		public static T ParseFrom(NetworkMessage msg)
		{
			return ParseFrom(msg.ReadMessage<SerializedPacket>());
		}

		[JsonIgnore]
		public SerializedPacket Serialized
		{
			get { return new SerializedPacket { Source = JsonConvert.SerializeObject(this) }; }
		}
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class PacketWithID : Attribute
	{
		public short PacketID { get; private set; }

		private PacketWithID() { }
		public PacketWithID(short packetID)
		{
			this.PacketID = packetID;
		}
	}
}
