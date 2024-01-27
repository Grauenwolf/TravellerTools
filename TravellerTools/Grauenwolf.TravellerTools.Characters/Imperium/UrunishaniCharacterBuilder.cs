namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class UrunishaniCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Dense atmosphere adaptation";
    public override string Species => "Urunishani";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Urunishani";
}
