using EnterSon.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace WSTichu.Client
{
	public class LobbyBoard : UIBoard
	{
		public event Action OnGameStartButtonClicked = delegate { };

		public void ClickGameStartButton()
		{
			OnGameStartButtonClicked?.Invoke();
		}

		[SerializeField]
		private Text _hostIPComponent = null;

		public string HostIP
		{
			get { return _hostIPComponent.text; }
		}
	}
}
