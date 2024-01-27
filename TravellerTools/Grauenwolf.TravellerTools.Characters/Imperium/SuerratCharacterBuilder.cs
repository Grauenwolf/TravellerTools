namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class SuerratCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Decreased comeliness\r\nEpigenetic height reduction\r\nHirsutism\r\nHyperesthesia\r\nIncreased strength\r\nTaint adaptation";
    public override string Species => "Suerrat";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Suerrat";
}
