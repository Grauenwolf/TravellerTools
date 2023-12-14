namespace Grauenwolf.TravellerTools.Characters;

public class SkillTemplate(string name, string? specialty = null)
{
    public SkillTemplate(string name, string? specialty, string? group) : this(name, specialty)
    {
        Group = group;
    }

    public string? Group { get; }
    public string Name { get; } = name;

    public string ShortName => Specialty ?? Name;
    public string? Specialty { get; } = specialty;

    public override string ToString()
    {
        if (!string.IsNullOrEmpty(Specialty))
        {
            if (!string.IsNullOrEmpty(Group))
                return $"{Name} ({Group}/{Specialty})";
            else
                return $"{Name} ({Specialty})";
        }
        else
        {
            if (!string.IsNullOrEmpty(Group))
                return $"{Name} ({Group})";
            else
                return Name;
        }
    }
}
