namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class LibertCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Desert adaptation\r\nAridity adaptation\r\nTrace atmosphere adaptation";
    public override string Species => "Libert";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Libert";
}
