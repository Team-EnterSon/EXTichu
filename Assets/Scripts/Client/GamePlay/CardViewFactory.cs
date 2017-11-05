using UnityEngine;

namespace EXTichu.Client
{
	public partial class CardView
	{
		public static class Factory
		{
			public static CardView Create(ClientCard card)
			{
				var view = Resources.Load<CardView>("Ingame/Prefabs/CardView");

				view.Number = card.Number;
				view.Shape  = card.Shape;
				view.Side   = card.Side;

				card.OnNumberChanged += value => view.Number = value;
				card.OnShapeChanged  += value => view.Shape  = value;
				card.OnSideChanged   += value => view.Side   = value;

				return view;
			}
		}
	}
}