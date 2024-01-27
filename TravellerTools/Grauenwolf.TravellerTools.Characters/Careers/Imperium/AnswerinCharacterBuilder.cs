namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class AnswerinCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Warrior race\r\nDigestive mutation\r\nHormonal mutation";
    public override string Species => "Answerin";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Answerin";
}
