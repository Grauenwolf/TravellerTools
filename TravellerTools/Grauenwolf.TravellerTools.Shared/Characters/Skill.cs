using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Characters
{
    public class Skill : EditableObjectModelBase
    {
        public Skill(string name, int level = 0)
        {
            Name = name;
            Level = level;
        }

        public string Name { get { return Get<string>(); } set { Set(value); } }

        public int Level { get { return Get<int>(); } set { Set(value); } }

    }
}
