using EnterSon.Stage;
using EnterSon.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace WSTichu.Client
{
	public class StageLobby : Stage
	{
		private LobbyBoard _lobbyBoard = null;

		public override void EnterStage()
		{
			base.EnterStage();

			_lobbyBoard = Instantiate(Resources.Load<LobbyBoard>("UIBoards/LobbyBoard"));
			_lobbyBoard.OnGameStartButtonClicked += onGameStartButtonClicked;
			CanvasController.Attach(_lobbyBoard);
		}

		private void onGameStartButtonClicked()
		{
			Func<string, bool> isValidHostIP = stringToValidate =>
			{
				System.Net.IPAddress _;
				return System.Net.IPAddress.TryParse(stringToValidate, out _);
			};

			if(isValidHostIP(_lobbyBoard.HostIP))
			{
				DataContainer.HostIP = _lobbyBoard.HostIP;
				setNextStage<StageGamePlay>();
			}
			else
			{
				// TODO(sorae): Show popup msg like "invalid ip address!"
				return;
			}
		}

		public override void ExitStage()
		{
			base.ExitStage();

			Destroy(_lobbyBoard.gameObject);
			_lobbyBoard = null;
		}
	}
}
