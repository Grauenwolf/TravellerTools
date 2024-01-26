namespace Grauenwolf.TravellerTools.Characters;

public class Skill
{
    public Skill(string name, int level)
        : this(name, null, level)
    {
    }

    public Skill(string name, string? specialty = null, int level = 0)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException($"{nameof(name)} is null or empty.", nameof(name));

        Specialty = specialty;
        Name = name;
        Level = level;
    }

    public string FullName
    {
        get
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

    public string? Group { get; set; }
    public bool IsPsionicTalent { get; set; }
    public int Level { get; set; }
    public string Name { get; set; }
    public string? Specialty { get; set; }

    public SkillTemplate ToSkillTemplate() => new(Name, Specialty, Group);

    public override string ToString()
    {
        if (!string.IsNullOrEmpty(Specialty))
        {
            if (!string.IsNullOrEmpty(Group))
                return $"{Name} ({Group}/{Specialty}) {Level}";
            else
                return $"{Name} ({Specialty}) {Level}";
        }
        else
        {
            if (!string.IsNullOrEmpty(Group))
                return $"{Name} ({Group}) {Level}";
            else
                return $"{Name} {Level}";
        }
    }
}
