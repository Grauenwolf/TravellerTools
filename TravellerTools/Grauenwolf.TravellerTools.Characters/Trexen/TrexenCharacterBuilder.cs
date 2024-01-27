using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.Trexen;

public class TrexenCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Trexen";
    public override string Species => "Trexen";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Trexen";
}