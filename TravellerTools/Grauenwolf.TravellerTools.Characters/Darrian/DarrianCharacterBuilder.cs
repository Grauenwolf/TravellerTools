using Grauenwolf.TravellerTools.Names;

namespace Grauenwolf.TravellerTools.Characters.Careers.Darrian;

public class DarrianCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : SpeciesCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override string? Faction => "Darrian Confederation";
    public override string? Source => "Aliens of Charted Space Vol. 3, page 36";
    public override string Species => "Darrian";
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
