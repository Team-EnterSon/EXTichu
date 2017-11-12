using System;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace EnterSon.Stage
{
	[CustomEditor(typeof(StageContainerBase), true)]
	public class StageContainerEditor : Editor
	{
		private int _startStageIndex = 0;

		public override void OnInspectorGUI()
		{ 
			base.OnInspectorGUI();

			drawFirstStageToRunField();

		}

		private void drawFirstStageToRunField()
		{
			var fieldName = "StartStage";

			var stageNames = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
								   from assemblyType in domainAssembly.GetTypes()
								   where assemblyType.IsSubclassOf(typeof(Stage))
								   select assemblyType.ToString()).ToList();

			var beutifiedStageNames = stageNames.Select(eachName => eachName.Split('.').Last());

			if(stageNames.Count() == 0)
			{
				EditorGUILayout.LabelField(fieldName, "There is no class inherits from Stage.");
				return;
			}

			_startStageIndex = stageNames.IndexOf(typeof(StageContainerBase).GetField("_startStageName", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(target) as string);
			if (_startStageIndex == -1)
				_startStageIndex = 0;
			// there are least 1 type of Stage
			_startStageIndex = EditorGUILayout.Popup(fieldName, _startStageIndex, beutifiedStageNames.ToArray());

			var selectedStageName = stageNames.ElementAt(_startStageIndex);
			var startStageField = typeof(StageContainerBase).GetField("_startStageName", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			startStageField.SetValue(target, selectedStageName);

			serializedObject.ApplyModifiedProperties();

			if(GUILayout.Button("Save Scene"))
			{
				EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
			}
		}
	}
}