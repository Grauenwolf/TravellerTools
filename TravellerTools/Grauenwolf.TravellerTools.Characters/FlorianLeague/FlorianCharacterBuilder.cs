using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.Careers.FlorianLeague;

public class FlorianCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Florian League";
    public override string? Remarks => "Low gravity";
    public override string Species => "Florian";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Florian";
}
