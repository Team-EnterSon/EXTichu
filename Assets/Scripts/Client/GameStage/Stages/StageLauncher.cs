using EnterSon.Stage;
using EnterSon.UI;
using System;
using System.Collections;
using UnityEngine;

namespace WSTichu.Client
{
	public class StageLauncher : Stage
	{
		private LauncherBoard _launcherBoard = null;

		public override void EnterStage()
		{
			base.EnterStage();

			_launcherBoard = Instantiate(Resources.Load<LauncherBoard>("UIBoards/LauncherBoard"));
			CanvasController.Attach(_launcherBoard);

			StartCoroutine(launcherRoutine());
		}

		public override void ExitStage()
		{
			base.ExitStage();
			Destroy(_launcherBoard.gameObject);
			_launcherBoard = null;
		}

		private IEnumerator launcherRoutine()
		{
			// TODO(sorae): patch routine and some initializations here
			yield return new WaitForSeconds(2.0f);

			setNextStage<StageLogin>();
		}
	}
}