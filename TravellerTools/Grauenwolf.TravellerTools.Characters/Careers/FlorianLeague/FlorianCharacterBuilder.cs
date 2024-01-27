using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.Careers.FlorianLeague;

public class FlorianCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Florian League";
    public override string? Remarks => "Low gravity";
    public override string Species => "Florian";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Florian";
}

public class HalkaCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Florian League";
    public override string? Remarks => "Radiation adaptation\r\nAlbino mutation";
    public override string Species => "Halka";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Halka";
}
