using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking;

namespace WSTichu.Common
{
	public class MessageType : MsgType
	{
		public const short CS_RequireGameBoard = 101;
		public const short SC_GameBoardDump = 102;

	}

	public class CS_RequireGameBoard : MessageBase
	{
	}

	public class SC_GameBoardDump : PacketBase<SC_GameBoardDump.Body>
	{
		public override Body Content { get; set; }
		public class Body
		{

			public GameBoard CurrentGameBoard;
			public List<RoundScore> RoundHistory;
		}
	}

	// ---- sub structures
	public class GameBoard
	{
		public Combination LastCombination = null;
		public List<Card> CardStack = null;
		public int? RequiredNumberByDog;
		public Dictionary<TeamType, Player[]> Players = null;
		public RoundPhase? CurrentRoundPhase;
		public List<int> PlayerUIDsInAction;
	}

	public enum RoundPhase
	{
		kFirstSpread = 0,
		kSecondSpread = 1,
		kExchangeCards = 2,
		kInRound = 3,

	}

	public enum TeamType
	{
		kTeam1 = 0,
		kTeam2 = 1,

	}

	public class Combination
	{
		private Combination() {}

		public bool IsCoverable(Combination target)
		{
			throw new NotImplementedException();
		}

		public static class Factory
		{
			public static Combination CreateFromCards(IEnumerable<Card> sourceCards)
			{
				throw new NotImplementedException();
			}
		}

		public enum CombinationType
		{
			kSingle = 0,
			kPair = 1,
			kContinuousPair = 2,
			kTriple = 3,
			kStraight = 4,
			kFullHouse = 5,
			kFourCard = 6,
			kStraightFlush = 7,
			kDog = 8,

		}

		public List<Card> Cards = null;
	}

	public class Card
	{
		public static readonly Card Unknown = new Card { Shape = null, TypeOfCard = null };

		public enum ShapeType
		{
			kSword = 0,
			kRing = 1,
			kStar = 2,
			kHouse = 3,

		}

		public enum CardType
		{
			kRegularCard = 0,
			kDragon      = 1,
			kPhonix      = 2,
			kDog         = 3,
			kBird		 = 4,

		}

		public ShapeType? Shape = null;
		public CardType? TypeOfCard = CardType.kRegularCard;


	}

	public class RoundScore
	{
		public Dictionary<TeamType, int> Scores = null;
	}

	public class Player
	{
		public int UID;
		public string Name = null;
		public TeamType Team = default(TeamType);
		
		public Card[] CardsInHand;

		public Card[] CardsEaten;

	}
}
