using Grauenwolf.TravellerTools.Names;

namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class AzhantiCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Shorter lifespan\r\nDense atmosphere adaptation\r\nHigh gravity\r\nHot climate adaptation\r\nIncreased strength\r\nTaint adaptation";
    public override string Species => "Azhanti";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Azhanti";
}
