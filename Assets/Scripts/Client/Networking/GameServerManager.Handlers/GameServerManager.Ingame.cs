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
		public event Action<SC_World> OnSC_World  = _ => 
		{
			Instance._onSC_Wrold.Invoke(_);
			Instance._onSC_Wrold = delegate { };
		};
		private event Action<SC_World> _onSC_Wrold = delegate { };

		public void SendCS_Hello(CS_Hello request, Action<SC_World> onReply)
		{
			this._onSC_Wrold += onReply;
			this.SendPacket(request);
		}
	}
}
