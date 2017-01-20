
namespace Grauenwolf.TravellerTools.Characters.Careers
{
    abstract class Career
    {
        protected Career(string name, string assignment)
        {
            Name = name;
            Assignment = assignment;
        }

        public string Name { get; }

        public string Assignment { get; }

        public abstract void Run(Character character, Dice dice);

        public abstract bool Qualify(Character character, Dice dice);
    }
}
