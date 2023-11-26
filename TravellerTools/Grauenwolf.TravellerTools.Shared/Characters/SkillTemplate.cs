namespace Grauenwolf.TravellerTools.Characters;

public class SkillTemplate(string name, string? specialty = null)
{
    public string Name { get; } = name;

    public string? Specialty { get; } = specialty;

    public override string ToString()
    {
        if (!string.IsNullOrEmpty(Specialty))
            return Name + " (" + Specialty + ") ";
        else
            return Name;
    }
}