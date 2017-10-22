using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterSon.UI
{
	public abstract class UIBoard : MonoBehaviour, IDisposable
	{
		public virtual void Dispose()
		{
			Destroy(gameObject);
		}
	}
}