using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.Careers.FlorianLeague;

public class HalkaCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Florian League";
    public override string? Remarks => "Radiation adaptation\r\nAlbino mutation";
    public override string Species => "Halka";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Halka";
}
