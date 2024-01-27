namespace Grauenwolf.TravellerTools.Characters;

public class CrewMember(CrewRole crewRole, Character character, string? skillName, int? skillLevel, string? title)
{
    public string CareerDetails => string.Join(" => ", Character.CareerHistory.Select(c => $"{c.Career}/{c.Assignment} [Terms {c.Terms}, Rank {c.Rank}{(c.CommissionRank > 0 ? "/" + c.CommissionRank : "")}]"));
    public string Careers => string.Join("/", Character.CareerHistory.Select(c => c.Career).Distinct());
    public Character Character { get; } = character;
    public CharacterBuilderOptions CharacterStub => Character.GetCharacterBuilderOptions();
    public CrewRole CrewRole { get; } = crewRole;
    public int? SkillLevel { get; } = skillLevel;
    public string? SkillName { get; } = skillName;
    public string Skills => string.Join(", ", Character.Skills.Where(s => s.Level > 0).Select(s => s.ToString()).OrderBy(s => s));
    public string? Title { get; } = title;
}
