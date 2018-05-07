//using System;

//namespace Grauenwolf.TravellerTools.TradeCalculator
//{
//	public class Mission
//	{
//		public bool IsTrap { get; set; }
//		public string Overview { get; set; }
//		public Patron Patron { get; set; }
//		public string Target { get; set; }
//		public Patron TargetPerson { get; set; }
//	}

//	public class MissionGenerator
//	{
//		public Mission GenerateMission(Dice dice)
//		{
//			var result = new Mission();
//			result.Patron = GeneratePatron(dice);

//			top:
//			switch (dice.D66())
//			{
//				case 11:
//					result.TargetPerson = GeneratePatron(dice);
//					result.Overview = $"Assassinate {result.TargetPerson.Name} a {result.TargetPerson.Type}";
//					break;

//				case 12:
//					result.TargetPerson = GeneratePatron(dice);
//					result.Overview = $"Frame {result.TargetPerson.Name} a {result.TargetPerson.Type}";
//					break;

//				case 13:
//					(result.Target, result.TargetPerson) = GenerateTarget(dice);
//					result.Overview = $"Frame {result.TargetPerson.Name} a {result.TargetPerson.Type}";
//					break;

//				case 41:
//					result.Overview = "Investigate a crime";
//					break;

//				case 66:
//					result.IsTrap = true;
//					goto top;
//			}

//			return result;
//			/*
//12 Frame a target 42 Investigate a theft
//13 Destroy a target  43 Investigate a murder
//14 Steal from a target 44 Investigate a mystery
//15 Aid in a burglary 45 Investigate a target
//16 Stop a burglary 46 Investigate an event
//21 Retrieve data or an
//object from a secure
//facility
//51 Join an expedition
//22 Discredit a target 52 Survey a planet
//23 Find a lost cargo 53 Explore a new system
//24 Find a lost person  54 Explore a ruin
//25 Deceive a target  55 Salvage a ship
//26 Sabotage a target 56 Capture a creature
//31 Transport goods 61 Hijack a ship
//32 Transport a person  62 Entertain a noble
//33 Transport data 63 Protect a target
//34 Transport goods
//secretly
//64 Save a target
//35 Transport goods
//quickly
//65 Aid a target
//36 Transport dangerous
//goods
//66 It is a trap – the
//Patron intends to
//betray the Traveller
//*/
//		}

//		Patron GeneratePatron(Dice dice)
//		{
//			throw new NotImplementedException();
//		}

//		(string target, Patron targetPerson) GenerateTarget(Dice dice)
//		{
//			throw new NotImplementedException();
//		}
//	}

//	public class Patron
//	{
//		public string Name { get; set; }
//		public string Type { get; set; }
//	}
//}