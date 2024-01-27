namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class VilaniImperiumCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Species => "Vilani, Imperium";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Vilani";
}
