using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking;

namespace WSTichu.Common
{
	public class MessageType : MsgType
	{
		public const short CS_Hello = 101;
		public const short SC_World = 102;

	}

	public class CS_Hello : MessageBase
	{
		public string Name;
	}

	public class SC_World : MessageBase
	{

	}
}
