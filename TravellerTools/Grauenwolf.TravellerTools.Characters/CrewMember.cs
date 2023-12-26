namespace Grauenwolf.TravellerTools.Characters;

public class CrewMember(CrewRole crewRole, CharacterBuilderOptions characterStub, string? skillName, int? skillLevel)
{
    public CharacterBuilderOptions CharacterStub { get; } = characterStub;
    public CrewRole CrewRole { get; } = crewRole;
    public int? SkillLevel { get; } = skillLevel;
    public string? SkillName { get; } = skillName;
}
