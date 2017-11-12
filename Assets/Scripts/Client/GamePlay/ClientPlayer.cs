using EXTichu.Common.CoreLogics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXTichu.Client
{
	public class ClientPlayer : Player
	{

		public static class Factory
		{
			public static ClientPlayer CreateFrom(Player source)
			{
				// TODO(sorae): create ClientPlayer from Player
				//				ClientPlayer contains HandView field
				//var clientPlayer = new 
				throw new NotImplementedException();
			}
		}
	}
}
