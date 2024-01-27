using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.Careers.KargolConfederation;

public class KargolCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Kargol Confederation";
    public override string? Remarks => "";
    public override string Species => "Kargol";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Kargol";
}
