namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class HanenCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Lack of significant body or facial hair\r\nAdapted for aquatic environment";
    public override string Species => "Hanen";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Hanen";
}
