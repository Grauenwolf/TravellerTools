namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class RonninCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Species => "Ronnin";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Ronnin";
}
