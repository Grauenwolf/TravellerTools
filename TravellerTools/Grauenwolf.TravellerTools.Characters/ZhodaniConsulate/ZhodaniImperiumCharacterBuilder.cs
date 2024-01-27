using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.ZhodaniConsulate;

public class ZhodaniImperiumCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Species => "Zhodani, Imperium";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Zhodani";
}