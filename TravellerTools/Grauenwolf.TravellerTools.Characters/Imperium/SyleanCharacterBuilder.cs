namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class SyleanCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Low gravity\r\nDense atmosphere adaptation\r\nEpigenetic height reduction";
    public override string Species => "Sylean";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Sylean";
}
