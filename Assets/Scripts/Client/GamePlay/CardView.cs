using EnterSon.Utilities;
using EXTichu.Common.CoreLogics;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EXTichu.Client
{
	public partial class CardView : MonoBehaviour
	{
		#region resource caches
		private static Dictionary<Tuple<Card.ShapeType, Card.NumberType>, Sprite> _cachedCardSprites
			= new Dictionary<Tuple<Card.ShapeType, Card.NumberType>, Sprite>();

		public static void LoadResources()
		{
			#region task definitions..
			Func<Card.ShapeType, Card.NumberType, string> determineFileName =
				(shape, number) =>
				{
					var lResult = string.Empty;
					if (shape == Card.ShapeType.kSpecial)
					{
						lResult += "S_";
						switch (number)
						{
							case Card.NumberType.kMahJong:
								lResult += "MahJong";
								break;
							case Card.NumberType.kDog:
								lResult += "Dog";
								break;
							case Card.NumberType.kPhoenix:
								lResult += "Phoenix";
								break;
							case Card.NumberType.kDragon:
								lResult += "Dragon";
								break;
							default:
								return null;
						}
					}
					else // shape is in range of 1~4
					{
						lResult += (int)shape;
						if (false == number.IsNormal())
							return null;
						else
							lResult += (int)number;
					}
					return lResult;
				};
			#endregion

			if (_cachedCardSprites.Count > 0)
			{
				Debug.LogWarningFormat("[{0}.{1}.{2}] Resources already loaded!",
					nameof(CardView), nameof(Factory), nameof(LoadResources));
				return;
			}

			var allShapes = Enum.GetValues(typeof(Card.ShapeType)).Cast<Card.ShapeType>();
			var allNumbers = Enum.GetValues(typeof(Card.NumberType)).Cast<Card.NumberType>();

			allShapes.ForEach(eachShape =>
			{
				allNumbers.ForEach(eachNumber =>
				{
					var spriteFilename = determineFileName(eachShape, eachNumber);
					if (false == string.IsNullOrEmpty(spriteFilename))
					{
						var loadedResource = Resources.Load<Sprite>("Cards/" + spriteFilename);
						if (loadedResource != null)
							_cachedCardSprites.Add(Tuple.Create(eachShape, eachNumber), loadedResource);
					}
				});
			});
		}

		public static void UnloadResources()
		{
			_cachedCardSprites.Values.ForEach(eachSprite => Resources.UnloadAsset(eachSprite));
			_cachedCardSprites.Clear();
		}
		#endregion

		public static void Initialize()
		{
			LoadResources();
		}

		[SerializeField]
		private SpriteRenderer _frontRenderer = null;
		[SerializeField]
		private GameObject _backSide          = null;
		[SerializeField]
		private GameObject _frontSide         = null;

		public Card.ShapeType? Shape
		{
			get { return _shapeBundle.Item1; }
			set
			{
				_shapeBundle = Tuple.Create(value, this.Number);
			}
		}
		public Card.NumberType? Number
		{
			get { return _shapeBundle.Item2; }
			set
			{
				_shapeBundle = Tuple.Create(this.Shape,value);
			}
		}
		public Card.SideType Side
		{
			get { return Side; }
			set
			{
				Side = value;
				this._backSide. SetActive(value == Card.SideType.kBack );
				this._frontSide.SetActive(value == Card.SideType.kFront);
			}
		}

		private Tuple<Card.ShapeType?, Card.NumberType?> _shapeBundle
		{
			get { return _shapeBundle; }
			set
			{
				_shapeBundle = value;
				if (value.Item1 == null || value.Item2 == null)
					return;
				else
					this.refreshFrontSprite(value.Item1.Value, value.Item2.Value);
			}
		}

		private void refreshFrontSprite(Tuple<Card.ShapeType, Card.NumberType> source)
		{
			// TODO(sorae): error log
			if (false == _cachedCardSprites.ContainsKey(source))
				return;

			this._frontRenderer.sprite = _cachedCardSprites[source];
		}
		private void refreshFrontSprite(Card.ShapeType shape, Card.NumberType number)
			=> refreshFrontSprite(Tuple.Create(shape, number));
	}
}
