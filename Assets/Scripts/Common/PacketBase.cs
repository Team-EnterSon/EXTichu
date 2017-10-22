using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace WSTichu.Common
{
	public abstract class PacketBase<T> : MessageBase
	{
		public abstract T Content { get; set; }

		public override void Deserialize(NetworkReader reader)
		{
			Content = JsonConvert.DeserializeObject<T>(reader.ReadString());
		}

		public override void Serialize(NetworkWriter writer)
		{
			writer.Write(JsonConvert.SerializeObject(Content));
		}
	}
}
