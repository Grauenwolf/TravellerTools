namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class AyanshiCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Long lifespan\r\nNight vision\r\nReproductive mutation";
    public override string Species => "Ayansh'i";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Ayansh%27i";
}
