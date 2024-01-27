using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.ThirdEmpireOfGashikan;

public class YileanCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)

{
    public override string Faction => "Third Empire of Gashikan";
    public override string? Remarks => "High gravity";
    public override string Species => "Yilean";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Yilean";
}