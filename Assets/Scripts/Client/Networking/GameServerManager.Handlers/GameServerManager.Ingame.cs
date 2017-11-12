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
		public event Action<SC_JoinMatch> OnSC_JoinMatch = _ =>
		{
			Instance._onSC_JoinMatch.Invoke(_);
			Instance._onSC_JoinMatch = delegate { };
		};

		public void SendCS_JoinMatch(CS_JoinMatch request, Action<SC_JoinMatch> onReply)
		{
			if(onReply != null)
				this._onSC_JoinMatch += onReply;
			this.SendPacket(request);
		}
	}
}
