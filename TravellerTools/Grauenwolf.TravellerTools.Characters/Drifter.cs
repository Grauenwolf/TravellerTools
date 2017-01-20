namespace Grauenwolf.TravellerTools.Characters
{
    internal class Drifter : Career
    {
        public Drifter() : base("Drifter")
        {
        }

        public override bool Qualify(Character character, Dice dice)
        {
            return true;
        }

        public override void Run(Character character, Dice dice)
        {
            //TODO
            character.CurrentTerm += 1;
        }
    }
}