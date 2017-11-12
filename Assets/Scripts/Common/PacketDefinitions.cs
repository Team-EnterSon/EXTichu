using EXTichu.Common.CoreLogics;
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
		public const short CS_JoinMatch = 111;
		public const short SC_JoinMatch = 112;
	}

	#region packet definitions
	[PacketWithID(MessageType.CS_JoinMatch)]
	public class CS_JoinMatch : Packet<CS_JoinMatch>
	{
		public string PlayerName = string.Empty;
	}

	[PacketWithID(MessageType.SC_JoinMatch)]
	public class SC_JoinMatch : Packet<SC_JoinMatch>
	{
		public GameDump GameContext = new GameDump();
	}
	#endregion

	#region data structures
	public struct GameDump
	{
		public Dictionary<PlayerPosition, Player> PlayersInGame;
	}
	#endregion
}