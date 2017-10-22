using EnterSon.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnterSon.Stage
{
	public abstract class StageContainerBase : MonoBehaviour
	{
		/// <summary>
		/// This method will invoked first before any stage's initialize method.
		/// </summary>
		protected abstract void bootUpAction();

		[SerializeField, HideInInspector]
		private string _startStageName = null;

		private bool _isInitialized = false;

		private Dictionary<Type, Stage> _stages = new Dictionary<Type, Stage>();
		public IEnumerable<Stage> Stages { get { return _stages.Values; } }

		private Stage _currentStage = null;

		public void OnEnable()
		{
			if (false == _isInitialized)
			{
				bootUpAction();
				initializeAllStages();
				_isInitialized = true;
			}
		}

		private void initializeAllStages()
		{
			var allTypesOfStage = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
								   from assemblyType in domainAssembly.GetTypes()
								   where assemblyType.IsSubclassOf(typeof(Stage))
								   select assemblyType).ToList();

			Action<Type> addStage = (stageType) =>
			{
			// TODO(sorae): Filter out types that are not stages.
			var newStage = this.gameObject.AddComponent(stageType) as Stage;
				if (newStage != null)
					this._stages.Add(stageType, newStage);
			};

			allTypesOfStage.ForEach(stageType => addStage(stageType));
			this.Stages.ForEach(eachStage => eachStage.InitializeStage());

			// TODO(sorae): start with launcher stage
			StartCoroutine(switchStage(Type.GetType(_startStageName)));
		}

		public void Update()
		{
			_currentStage.UpdateStage(Time.deltaTime);
			if (_currentStage.NextStageType != null)
				StartCoroutine(switchStage(_currentStage.NextStageType));
		}

		private IEnumerator switchStage(Type nextStageType)
		{
			Debug.Assert(nextStageType != null);
			Debug.Assert(_stages.ContainsKey(nextStageType));

			// NOTE(sorae): current stage can be null when launch
			_currentStage?.ExitStage();
			_currentStage = _stages[nextStageType];
			yield return null;
			_currentStage.EnterStage();
			yield return null;

			yield return clearMemoryRoutine();
		}

		private IEnumerator clearMemoryRoutine()
		{
			// NOTE(sorae): Delay 1 frame to be assured that references has been cleaned up
			yield return null;
			Resources.UnloadUnusedAssets();
		}
	}
}