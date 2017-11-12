using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXTichu.Common.CoreLogics
{
	public class Player
	{
		public virtual string Name { get; set; } = string.Empty;
		public virtual TeamType Team { get; set; }
		public virtual PlayerPosition Position { get; set; }
		public virtual uint UID { get; set; }
		public virtual List<Card> CardsInHand { get; set; } = new List<Card>();
	}
}
