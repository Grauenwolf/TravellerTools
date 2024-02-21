using Grauenwolf.TravellerTools.Characters;

namespace Grauenwolf.TravellerTools.Encounters;

public class EncounterCharacter(string encounterRole, Character character)
{
    public string BestSkills
    {
        get
        {
            var bestLevel = Character.Skills.BestSkillLevel();
            return string.Join(", ", Character.Skills.Where(s => s.Level == bestLevel).Select(s => s.ToString()).OrderBy(s => s));
        }
    }

    public string Careers => string.Join("/", Character.CareerHistory.Select(c => c.Career).Distinct());
    public Character Character { get; } = character;
    public CharacterBuilderOptions CharacterStub => Character.GetCharacterBuilderOptions();
    public string EncounterRole { get; } = encounterRole;
    public string Skills => string.Join(", ", Character.Skills.Where(s => s.Level > 0).Select(s => s.ToString()).OrderBy(s => s));
}
