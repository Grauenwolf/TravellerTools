namespace Grauenwolf.TravellerTools.Characters;

public class CrewMember(CrewRole crewRole, CharacterBuilderOptions characterStub, string? skillName, int? skillLevel, string? title)
{
    public CharacterBuilderOptions CharacterStub { get; } = characterStub;
    public CrewRole CrewRole { get; } = crewRole;
    public int? SkillLevel { get; } = skillLevel;
    public string? SkillName { get; } = skillName;
    public string? Title { get; } = title;
}
