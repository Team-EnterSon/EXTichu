using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EnterSon.UI
{
	public static class CanvasController
	{
		private static Canvas _cachedCanvas = null;
		public static Canvas UICanvas
		{
			get
			{
				if(_cachedCanvas == null)
				{
					// TODO(sorae): Handle exception
					_cachedCanvas = GameObject.FindObjectOfType<Canvas>();
				}

				return _cachedCanvas;
			}
		}

		public static void Attach(UIBoard board)
		{
			var rt = board.transform as RectTransform;
			board.transform.SetParent(UICanvas.transform);
			rt.anchorMin = Vector2.zero;
			rt.anchorMax = Vector2.one;
			rt.sizeDelta = Vector2.zero;
			rt.localScale = Vector3.one;
		}
	}
}
