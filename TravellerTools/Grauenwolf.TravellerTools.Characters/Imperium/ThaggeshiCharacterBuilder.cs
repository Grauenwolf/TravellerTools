namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class ThaggeshiCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Increased genetic variation\r\nDental mutation\r\nDigestive adaptation\r\nIncreased strength\r\nLower gravity\r\nRadiation adaptation";
    public override string Species => "Thaggeshi";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Thaggeshi";
}
