using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

public class MurrissiCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Aslan Hierate";
    public override string? Remarks => "Desert adaptation\r\nAridity adaptation\r\nDermal mutation\r\nHot climate adaptation\r\nLong lifespan\r\nPyschological mutation";
    public override string Species => "Murrissi";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Murrissi";
}
