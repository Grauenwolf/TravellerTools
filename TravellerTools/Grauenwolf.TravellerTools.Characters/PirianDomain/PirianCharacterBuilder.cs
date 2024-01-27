using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.Careers.PirianDomain;

public class PirianCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Pirian Domain";
    public override string? Remarks => "Only aesthetic and mental changes from Solomani\r\nSome use Vargr-style Charisma";
    public override string Species => "Pirian";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Pirian";
}
