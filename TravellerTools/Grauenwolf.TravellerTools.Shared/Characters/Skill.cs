namespace Grauenwolf.TravellerTools.Characters;

public class Skill
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

    public int Level { get; set; }
    public string Name { get; set; }
    public string? Specialty { get; set; }

    public override string ToString()
    {
        if (Level > 0 && !string.IsNullOrEmpty(Specialty))
            return Name + " (" + Specialty + ") " + Level;
        else
            return Name + " " + Level;
    }
}
