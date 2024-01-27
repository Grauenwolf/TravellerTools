namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class SolomaniImperiumCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Species => "Solomani, Imperium";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Solomani";
}
