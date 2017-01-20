using System;
using System.Linq;

namespace Grauenwolf.TravellerTools.Characters
{
    abstract class Career
    {
        protected Career(string name)
        {
            Name = name;
        }
        public string Name { get; }

        public abstract void Run(Character character, Dice dice);
        public abstract bool Qualify(Character character, Dice dice);
    }
}
