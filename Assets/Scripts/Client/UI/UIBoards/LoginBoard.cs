using System;
using EnterSon.UI;
using UnityEngine;
using UnityEngine.UI;

namespace WSTichu.Client
{

	public class LoginBoard : UIBoard
	{
		public event Action OnLoginButtonClicked = delegate { };

		public void ClickLoginButton()
		{
			OnLoginButtonClicked?.Invoke();
		}

		[SerializeField]
		private Text _nicknameComponent = null;

		public string Nickname
		{
			get { return _nicknameComponent.text; }
		}
	}
}
