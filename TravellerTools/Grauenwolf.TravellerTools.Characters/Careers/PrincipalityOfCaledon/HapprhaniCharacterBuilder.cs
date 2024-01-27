using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.Careers.PrincipalityOfCaledon;

public class HapprhaniCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Principality of Caledon";
    public override string? Remarks => "Thin atmosphere adaptation\r\nDigestive mutation";
    public override string Species => "Happrhani";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Happrhani";
}
