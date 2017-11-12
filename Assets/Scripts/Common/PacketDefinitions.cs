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
		public const short CS_SetReady = 113;
		public const short SC_SetReady = 114;
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

	[PacketWithID(MessageType.CS_SetReady)]
	public class CS_SetReady : Packet<CS_SetReady>
	{
		public bool IsReady;
	}

	[PacketWithID(MessageType.SC_SetReady)]
	public class SC_SetReady : Packet<SC_SetReady>
	{
		public uint PlayerUID;
		public bool IsReady;
	}

	#endregion

	#region data structures
	public struct GameDump
	{
		public Dictionary<PlayerPosition, Player> PlayersInGame;
	}
	#endregion
}