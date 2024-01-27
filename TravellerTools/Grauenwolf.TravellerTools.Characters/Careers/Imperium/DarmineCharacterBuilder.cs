using Grauenwolf.TravellerTools.Names;

namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class DarmineCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Dense atmosphere adaptation\r\nRadiation resistance\r\nTaint adaptation";
    public override string Species => "Darmine";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Darmine_(race)";
}
