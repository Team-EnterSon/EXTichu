using EnterSon.Stage;
using EnterSon.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WSTichu.Client
{
	public class StageLogin : Stage
	{
		private LoginBoard _loginBoard = null;

		public override void EnterStage()
		{
			base.EnterStage();

			_loginBoard = Instantiate(Resources.Load<LoginBoard>("UIBoards/LoginBoard"));
			_loginBoard.OnLoginButtonClicked += onLoginButtonClicked;
			CanvasController.Attach(_loginBoard);

		}

		private void onLoginButtonClicked()
		{
			Func<string, bool> isValidNickname = nicknameToValidate =>
			{
				return !string.IsNullOrWhiteSpace(nicknameToValidate);
			};

			if (false == isValidNickname(_loginBoard.Nickname))
			{
				// TODO(sorae): Show popup message like - "Invalid nickname!"
				return;
			}

			DataContainer.MyNickname = _loginBoard.Nickname;
			login();
		}

		private void login()
		{
			setNextStage<StageLobby>();
		}

		public override void ExitStage()
		{
			base.ExitStage();

			Destroy(_loginBoard.gameObject);
			_loginBoard = null;
		}
	}
}
