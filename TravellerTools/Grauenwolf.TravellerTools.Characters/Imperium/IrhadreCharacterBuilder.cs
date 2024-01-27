namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class IrhadreCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Species => "Irhadre";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Irhadre";
}
