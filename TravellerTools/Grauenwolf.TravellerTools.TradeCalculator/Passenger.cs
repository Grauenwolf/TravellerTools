using Grauenwolf.TravellerTools.Characters;

namespace Grauenwolf.TravellerTools.TradeCalculator
{
    public class Passenger : Person
    {
        public string PassengerType { get; set; }
        public string TravelType { get; set; }
        public int? Seed { get; set; }
        public string Skills { get; set; }



        public static void AddPassengerType(Passenger passenger, Dice random)
        {
            int roll1 = random.D66();
            int roll2 = random.D(6);
            int roll3 = random.D(6);

            switch (roll1)
            {
                case 11: passenger.PassengerType = "Refugee - political"; return;
                case 12: passenger.PassengerType = "Refugee - economic"; return;
                case 13: passenger.PassengerType = "Starting a new life offworld"; return;
                case 14: passenger.PassengerType = "Mercenary"; return;
                case 15: passenger.PassengerType = "Spy"; return;
                case 16: passenger.PassengerType = "Corporate Mechanic"; return;
                case 21: passenger.PassengerType = "Out to see the universe"; return;
                case 22: passenger.PassengerType = roll2 <= 3 ? "Tourist (Irritating)" : "Tourist (Charming)"; return;
                case 23: passenger.PassengerType = "Wide-eyed Yokel"; return;
                case 24: passenger.PassengerType = "Adventurer"; return;
                case 25: passenger.PassengerType = "Explorer"; return;
                case 26: passenger.PassengerType = "Claustrophobic"; return;
                case 31: passenger.PassengerType = "Expectant Mother"; return;
                case 32: passenger.PassengerType = "Wants to stowaway or join the crew"; return;
                case 33: passenger.PassengerType = "Possess something illegal or dangerous"; return;
                case 34:

                    if (roll2 <= 3)
                        passenger.PassengerType = "Causes Trouble (Drunk)";
                    else if (roll2 <= 5)
                        passenger.PassengerType = "Causes Trouble (Violent)";
                    else
                        passenger.PassengerType = "Causes Trouble (Insane)";
                    return;
                case 35: passenger.PassengerType = "Unusually Pretty or Handsome"; return;
                case 36: passenger.PassengerType = string.Format("Engineer (Engineer {0}, Mechanic {1})", (roll2 - 1), (roll3 - 1)); return;
                case 41: passenger.PassengerType = "Ex-scout"; return;
                case 42: passenger.PassengerType = "Wanderer"; return;
                case 43: passenger.PassengerType = "Thief or other criminal"; return;
                case 44: passenger.PassengerType = "Scientist"; return;
                case 45: passenger.PassengerType = "Journalist or researcher"; return;
                case 46: passenger.PassengerType = string.Format("Entertainer (Steward {0}, Perform {1})", (roll2 - 1), (roll3 - 1)); return;
                case 51: passenger.PassengerType = string.Format("Gambler (Gambler {0})", (roll2 - 1)); return;
                case 52: passenger.PassengerType = "Rich nobel - complains a lot"; return;
                case 53: passenger.PassengerType = "Rich nobel - eccentric"; return;
                case 54: passenger.PassengerType = "Rich nobel - raconteur"; return;
                case 55: passenger.PassengerType = "Diplomat on a mission"; return;
                case 56: passenger.PassengerType = "Agent on a mission"; return;
                case 61:
                    passenger.IsPatron = true;
                    passenger.PassengerType = "Patron";
                    return;
                case 62: passenger.PassengerType = "Alien"; return;
                case 63: passenger.PassengerType = "Bounty hunter"; return;
                case 64: passenger.PassengerType = "On the run"; return;
                case 65: passenger.PassengerType = "Wants to board the ship for some reason"; return;
                case 66: passenger.PassengerType = "Hijacker or pirate"; return;
            }
        }
    }
}
