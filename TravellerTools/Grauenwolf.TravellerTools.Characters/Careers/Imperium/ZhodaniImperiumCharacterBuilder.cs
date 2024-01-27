using Grauenwolf.TravellerTools.Names;

namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class ZhodaniImperiumCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Species => "Zhodani, Imperium";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Zhodani";
}
