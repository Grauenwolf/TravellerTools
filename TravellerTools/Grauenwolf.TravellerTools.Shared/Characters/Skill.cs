using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Characters
{
    public class SkillTemplate
    {
        public SkillTemplate(string name, string? specialty = null)
        {
            Specialty = specialty;
            Name = name;
        }

        public string Name { get; }

        public string? Specialty { get; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Specialty))
                return Name + " (" + Specialty + ") ";
            else
                return Name;
        }
    }

    public class Skill : EditableObjectModelBase
    {
        public Skill(string name, int level = 0)
            : this(name, null, level)
        {
        }

        public Skill(string name, string? specialty = null, int level = 0)
        {
            Specialty = specialty;
            Name = name;
            Level = level;
        }

        public string Name { get => Get<string>(); set => Set(value); }

        public int Level { get => Get<int>(); set => Set(value); }
        public string? Specialty { get => Get<string?>(); set => Set(value); }

        public override string ToString()
        {
            if (Level > 0 && !string.IsNullOrEmpty(Specialty))
                return Name + " (" + Specialty + ") " + Level;
            else
                return Name + " " + Level;
        }
    }
}
