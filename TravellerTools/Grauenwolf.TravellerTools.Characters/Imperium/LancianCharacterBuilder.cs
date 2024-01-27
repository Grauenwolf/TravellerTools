namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class LancianCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Species => "Lancian";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Lancian";
}
