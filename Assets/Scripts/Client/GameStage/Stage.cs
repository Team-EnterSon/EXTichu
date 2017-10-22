using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnterSon.Stage
{
	public abstract class Stage : MonoBehaviour
	{
		public Type NextStageType { get; private set; } = null;

		public virtual void InitializeStage()
		{
			Debug.LogFormat("[Stage] Initializing <color=blue>{0}</color>...", this.GetType().ToString().Split('.').Last());

		}

		public virtual void EnterStage()
		{
			Debug.LogFormat("[Stage] Enter stage - <color=blue>{0}</color>...", this.GetType().ToString().Split('.').Last());

		}

		public virtual void ExitStage()
		{
			Debug.LogFormat("[Stage] Exit stage - <color=red>{0}</color>...", this.GetType().ToString().Split('.').Last());

		}

		public virtual void UpdateStage(float dt)
		{

		}

		protected void setNextStage<TTargetStage>() where TTargetStage : Stage
		{
			Debug.LogFormat("[Stage] Set next stage to <color=blue>{0}</color>", typeof(TTargetStage).ToString().Split('.').Last());
			NextStageType = typeof(TTargetStage);
		}
	}
}