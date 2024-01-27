using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.KhuurLeague;

public class SyditeCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Khuur League";
    public override string? Remarks => "AKA Sydymites\r\nEpigenetic height increase\r\nHeavy gravity\r\nIncreased strength\r\nPolymelia (4-armed)\r\nThin atmosphere adaptation\r\nWater adaptation";
    public override string Species => "Sydite";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Sydite";
}