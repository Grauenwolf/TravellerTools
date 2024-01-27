using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.LiberatedStars;

public class ZarisCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Liberated Stars";
    public override string Species => "Zaris";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Zaris";
}