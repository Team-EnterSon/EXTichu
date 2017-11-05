// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using UnityEngine.Networking;
// 
// namespace EXTichu.Common
// {
// 	public class MessageType : MsgType
// 	{
// 		public const short CS_RequestGameBoard	= 101;
// 		public const short SC_GameBoardDump		= 102;
// 		public const short SC_ConnectSuccess	= 103;
// 		public const short SC_UpdateReadiness	= 104;
// 	}
// 
// 	public class CS_RequestGameBoard : Packet<CS_RequestGameBoard>
// 	{
// 	}
// 
// 	public class SC_GameBoardDump : Packet<SC_GameBoardDump>
// 	{
// 		public GameBoard CurrentGameBoard;
// 		public List<RoundScore> RoundHistory;
// 	}
// 
// 	public class SC_ConnectSuccess : Packet<SC_ConnectSuccess>
// 	{
// 		public Player player;
// 	}
// 
// 	public class SC_UpdateReadinesses : Packet<SC_UpdateReadinesses>
// 	{
// 		public Dictionary<uint, bool> IsPlayersReady;
// 	}
// 
// 	// ---- sub structures
// 	public class GameBoard
// 	{
// 		public Combination LastCombination = null;
// 		public List<CardInfo> CardStack = null;
// 		public int? RequiredNumberByDog;
// 		public List<Player> Players = null;
// 		public RoundPhase? CurrentRoundPhase;
// 		public List<int> PlayerUIDsInAction;
// 	}
// 
// 	public enum RoundPhase
// 	{
// 		kFirstSpread = 0,
// 		kSecondSpread = 1,
// 		kExchangeCards = 2,
// 		kInRound = 3,
// 
// 	}
// 
// 	public enum TeamType
// 	{
// 		kTeamBegin = kTeam1,
// 		kTeam1 = 0,
// 		kTeam2 = 1,
// 		kTeamEnd,
// 	}
// 
// 	public class Combination
// 	{
// 		private Combination() {}
// 
// 		public bool IsCoverable(Combination target)
// 		{
// 			throw new NotImplementedException();
// 		}
// 
// 		public static class Factory
// 		{
// 			public static Combination CreateFromCards(IEnumerable<CardInfo> sourceCards)
// 			{
// 				return new Combination { Cards = sourceCards.ToList() };
// 			}
// 		}
// 
// 		public enum CombinationType
// 		{
// 			kSingle = 0,
// 			kPair = 1,
// 			kContinuousPair = 2,
// 			kTriple = 3,
// 			kStraight = 4,
// 			kFullHouse = 5,
// 			kFourCard = 6,
// 			kStraightFlush = 7,
// 			kDog = 8,
// 
// 		}
// 
// 		public List<CardInfo> Cards = null;
// 	}
// 
// 	public class CardInfo
// 	{
// 		public static readonly CardInfo Unknown = new CardInfo { Shape = null, TypeOfCard = null };
// 
// 		public enum ShapeType
// 		{
// 			kSword = 0,
// 			kRing = 1,
// 			kStar = 2,
// 			kHouse = 3,
// 
// 		}
// 
// 		public enum CardType
// 		{
// 			kRegularCard = 0,
// 			kDragon      = 1,
// 			kPhonix      = 2,
// 			kDog         = 3,
// 			kBird		 = 4,
// 
// 		}
// 
// 		public ShapeType? Shape = null;
// 		public CardType? TypeOfCard = CardType.kRegularCard;
// 	}
// 
// 	public class RoundScore
// 	{
// 		public Dictionary<TeamType, int> Scores = null;
// 	}
// 
// 	public class Player
// 	{
// 		public bool? IsReady;
// 		public int UID;
// 		public string Name = null;
// 		public TeamType Team = default(TeamType);
// 		
// 		public CardInfo[] CardsInHand;
// 
// 		public CardInfo[] CardsEaten;
// 	}
// }
