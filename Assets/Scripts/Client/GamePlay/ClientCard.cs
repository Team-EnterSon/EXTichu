using System;
using EXTichu.Common;
using EXTichu.Common.CoreLogics;

namespace EXTichu.Client
{
	public class ClientCard : Card
	{
		private ClientCard() { }

		public override Player Owner
		{
			get
			{
				// TODO(sorae): implementation..

				return null;
			}
		}

		private CardView _cardView { get; set; }

		public event Action<ShapeType?> OnShapeChanged = delegate { };
		public override ShapeType? Shape
		{
			get { return base.Shape; }
			set
			{
				base.Shape = value;
				OnShapeChanged(value);
			}
		}
		public event Action<SideType> OnSideChanged = delegate { };
		public override SideType Side
		{
			get { return base.Side; }
			set
			{
				base.Side = value;
				OnSideChanged(value);
			}
		}
		public event Action<NumberType?> OnNumberChanged = delegate { };
		public override NumberType? Number
		{
			get { return base.Number; }
			set
			{
				base.Number = value;
				OnNumberChanged(value);
			}
		}


		public static class Factory
		{
			public static ClientCard Create()
			{
				var instance = new ClientCard();
				instance._cardView = CardView.Factory.Create(instance);
				return instance;
			}
		}
	}
}
