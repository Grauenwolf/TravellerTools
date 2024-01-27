using Grauenwolf.TravellerTools.Names;

namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class CassilldanCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Low gravity\r\nGreater height\r\nTaint adaptation";
    public override string Species => "Cassilldan";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Cassilldan";
}
