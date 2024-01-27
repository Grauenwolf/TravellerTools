namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class TapazmalCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Low gravity\r\nDense atmosphere adaptation\r\nEpigenetic height reduction\r\nHot climate adaptation\r\nLonger lifespan\r\nRadiation mutation\r\nReproductive mutation";
    public override string Species => "Tapazmal";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Tapazmal";
}
