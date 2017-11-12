using EXTichu.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXTichu.Client
{
	public partial class GameServerManager
	{
		private event Action<SC_JoinMatch> _onSC_JoinMatch = delegate { };
		public event Action<SC_JoinMatch> OnSC_JoinMatch = packet =>
		{
			Instance._onSC_JoinMatch.Invoke(packet);
			Instance._onSC_JoinMatch = delegate { };
		};
		public void SendCS_JoinMatch(CS_JoinMatch request, Action<SC_JoinMatch> onReply = null)
		{
			if(onReply != null)
				this._onSC_JoinMatch += onReply;
			this.SendPacket(request);
		}

		private event Action<SC_SetReady> _onSC_SetReady = delegate { };
		public event Action<SC_SetReady> OnSC_SetReady = packet =>
		{
			Instance._onSC_SetReady.Invoke(packet);
			Instance._onSC_SetReady = delegate { };
		};
		public void SendCS_SetReady(CS_SetReady request, Action<SC_SetReady> onReply = null)
		{
			if (onReply != null)
				this._onSC_SetReady += onReply;
			this.SendPacket(request);
		}
	}
}
