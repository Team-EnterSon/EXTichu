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
	public class SourcePacket : MessageBase
	{
		public string Source = default(string);
	}

	public abstract class Packet<T>
	{
		public static T ParseFrom(SourcePacket source)
		{
			return JsonConvert.DeserializeObject<T>(source.Source);
		}

		public static T ParseFrom(NetworkMessage msg)
		{
			return ParseFrom(msg.ReadMessage<SourcePacket>());
		}

		public SourcePacket ToPacket()
		{
			return new SourcePacket { Source = JsonConvert.SerializeObject(this) };
		}
	}
}
