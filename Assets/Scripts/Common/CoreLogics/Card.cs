using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXTichu.Common.CoreLogics
{
	public abstract class Card
	{
		public enum ShapeType
		{
			kSpecial = 0,
			k1       = 1,
			k2       = 2,
			k3       = 3,
			k4       = 4,
		}

		public enum NumberType
		{
			kMahJong = 1,
			kTwo = 2,
			kThree =3,
			kFour = 4,
			kFive = 5,
			kSix = 6,
			kSeven = 7,
			kEight = 8,
			kNine = 9,
			kTen = 10,
			kJack = 11,
			kQueen = 12,
			kKing = 13,
			kAce = 14,
			kDog = 101,
			kPhoenix = 102,
			kDragon = 103,
		}

		public enum SideType
		{
			kBack  = 0,
			kFront = 1,
		}

		public abstract Player Owner { get; }
		public virtual ShapeType? Shape { get; set; }
		public virtual NumberType? Number { get; set; }
		public virtual SideType Side { get; set; }

	}

	public static class CardExtensions
	{
		public static bool IsNormal(this Card.NumberType me)
		{
			switch (me)
			{
				case Card.NumberType.kMahJong:
				case Card.NumberType.kDog:
				case Card.NumberType.kPhoenix:
				case Card.NumberType.kDragon:
					return false;
				default:
					return true;
			}
		}
	}
}
