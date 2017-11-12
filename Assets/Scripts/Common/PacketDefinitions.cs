using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace EXTichu.Common
{
	public static class MessageType
	{
#if DEBUG
		public const short CS_Hello = 101;
		public const short SC_World = 102;
#endif
	}

	[PacketWithID(MessageType.CS_Hello)]
	public class CS_Hello : Packet<CS_Hello>
	{
		public string Name = "Anonymous";
	}
	
	[PacketWithID(MessageType.SC_World)]
	public class SC_World : Packet<SC_World>
	{
		public DateTime Time = DateTime.Now;
	}
}