namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class TraltarCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Desert adaptation\r\nAridity adaptation\r\nDense atmosphere adaptation\r\nTaint adaptation";
    public override string Species => "Traltar";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Traltar";
}
