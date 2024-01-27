using Grauenwolf.TravellerTools.Characters.Careers.Imperium;

namespace Grauenwolf.TravellerTools.Characters.SwanfeiFreeWorlds;

public class SwanfehCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Swanfei Free Worlds";
    public override string Species => "Swanfeh";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Swanfeh";
}