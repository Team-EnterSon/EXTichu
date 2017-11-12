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
		public const short Connect = 32;
		public const short SC_GameboardDump = 111;
	}

	[PacketWithID(MessageType.SC_GameboardDump)]
	public class SC_GameboardDump : Packet<SC_GameboardDump>
	{
		public Player[] PlayersInGame;
	}
}