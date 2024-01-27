using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.HegemonyOfLorean;

public class KedepuCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Hegemony of Lorean";
    public override string? Remarks => "Technophile";
    public override string Species => "Kedepu";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Kedepu";
}