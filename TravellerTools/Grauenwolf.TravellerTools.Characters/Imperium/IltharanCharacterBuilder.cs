namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class IltharanCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Water adaptation\r\nCold climate adaptation\r\nNight vision";
    public override string Species => "Iltharan";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Iltharan";
}
