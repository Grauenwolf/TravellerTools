namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class VanejeneseCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Dense atmosphere adaptation\r\nCold climate adpatation\r\nSalinity adaptation";
    public override string Species => "Vanejenese";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Vanejenese";
}
