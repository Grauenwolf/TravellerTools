namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class GeoneeCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "High gravity\r\nDense atmosphere adaptation\r\nTechnophilia";
    public override string Species => "Geonee";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Geonee";
}