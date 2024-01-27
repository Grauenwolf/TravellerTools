using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.Careers.PeopleOfTheKraeda;

public class NeKraedaRenKelvaCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "People of the Kraeda";
    public override string? Remarks => "";
    public override string Species => "Ne Kraeda ren Kelva";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Ne_Kraeda_ren_Kelva";
}
