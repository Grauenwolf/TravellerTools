using Grauenwolf.TravellerTools.Names;

namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class DarrianImperiumCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Daryen\r\nLow gravity\r\nCold climate adaptation\r\nDigestive mutation\r\nRadiation adaptation\r\nTechnophilia";
    public override string Species => "Darrian, Imperium";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Daryen";

    protected override void InitialCharacterStats(Dice dice, Character character)
    {
        base.InitialCharacterStats(dice, character);
        character.Strength += -1;
        character.Dexterity += 1;
        character.Endurance += -1;
        character.Intellect += 1;
        character.Education += 1;
    }
}
