using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Characters
{
    public class PatronBuilder
    {
        string PickMission(Dice dice)
        {
            switch (dice.D66())
            {
                case 11: return "Assassinate a " + PickTarget(dice);
                case 12: return "Frame a " + PickTarget(dice);
                case 13: return "Destroy a " + PickTarget(dice);
                case 14: return "Steal from a " + PickTarget(dice);
                case 15: return "Aid in a burglary";
                case 16: return "Stop a burglary";
                case 21: return "Retrieve data or an object from a secure facility";
                case 22: return "Discredit a " + PickTarget(dice);
                case 23: return "Find a lost cargo";
                case 24: return "Find a lost person";
                case 25: return "Deceive a " + PickTarget(dice);
                case 26: return "Sabotage a " + PickTarget(dice);
                case 31: return "Transport goods";
                case 32: return "Transport a person";
                case 33: return "Transport data";
                case 34: return "Transport goods secretly";
                case 35: return "Transport goods quickly";
                case 36: return "Transport dangerous goods";
                case 41: return "Investigate a crime";
                case 42: return "Investigate a theft";
                case 43: return "Investigate a murder";
                case 44: return "Investigate a mystery";
                case 45: return "Investigate a " + PickTarget(dice);
                case 46: return "Investigate an event";
                case 51: return "Join an expedition";
                case 52: return "Survey a planet";
                case 53: return "Explore a new system";
                case 54: return "Explore a ruin";
                case 55: return "Salvage a ship";
                case 56: return "Capture a creature";
                case 61: return "Hijack a ship";
                case 62: return "Entertain a noble";
                case 63: return "Protect a " + PickTarget(dice);
                case 64: return "Save a " + PickTarget(dice);
                case 65: return "Aid a " + PickTarget(dice);
                case 66: return "It is a trap – the Patron intends objectbetray the Traveller";
            }
            return "";
        }
        string PickTarget(Dice dice)
        {
            switch (dice.D66())
            {
                case 11: return "Common Trade Goods";
                case 12: return "Common Trade Goods";
                case 13: return "Random Trade Goods";
                case 14: return "Random Trade Goods";
                case 15: return "Illegal Trade Goods";
                case 16: return "Illegal Trade Goods";
                case 21: return "Computer Data";
                case 22: return "Alien Artefact";
                case 23: return "Personal Effects";
                case 24: return "Work of Art";
                case 25: return "Historical Artefact";
                case 26: return "Weapon";
                case 31: return "Starport";
                case 32: return "Asteroid Base";
                case 33: return "City";
                case 34: return "Research station";
                case 35: return "Bar or Nightclub";
                case 36: return "Medical Facility";
                case 41: return "Roll on the Random Patron table";
                case 42: return "Roll on the Random Patron table";
                case 43: return "Roll on the Random Patron table";
                case 44: return "Roll on the Allies and Enemies table";
                case 45: return "Roll on the Allies and Enemies table";
                case 46: return "Roll on the Allies and Enemies table";
                case 51: return "Local Government";
                case 52: return "Planetary Government";
                case 53: return "Corporation";
                case 54: return "Imperial Intelligence";
                case 55: return "Criminal Syndicate";
                case 56: return "Criminal Gang";
                case 61: return "Free Trader";
                case 62: return "Yacht";
                case 63: return "Cargo Hauler";
                case 64: return "Police Cutter";
                case 65: return "Space Station";
                case 66: return "Warship";
            }
            return "";
        }
    }
    public class Patron
    {
        public string Mission { get; set; }
    }
}
