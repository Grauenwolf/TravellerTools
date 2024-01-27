using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.ZhodaniConsulate;

public class VlazhdumectaCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Zhodani Consulate";
    public override string? Remarks => "Dense atmosphere adaptation\r\nCold climate adpatation\r\nSalinity adaptation\r\n";
    public override string Species => "Vlazhdumecta";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Vlazhdumecta";
}