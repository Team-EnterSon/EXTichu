using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSTichu.Common;

namespace WSTichu.Server
{
	public static class GameBoardExtensions
	{
		public static TeamType GetAvailableTeam(this GameBoard me)
		{
			TeamType selectedTeam = default(TeamType);
			int currentTeamCount = int.MaxValue;

			foreach(var pair in me.Players)
			{
				if(currentTeamCount > pair.Value.Count<Player>())
				{
					selectedTeam = pair.Key;
					currentTeamCount = pair.Value.Count<Player>();
				}
			}

			return selectedTeam;
		}
	}
}
