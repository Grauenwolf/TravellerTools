using Grauenwolf.TravellerTools.Names;

namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class CafadanCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Shorter lifespans\r\nCold climate adaptation\r\nNight vision\r\nTroglobytic adaptation";
    public override string Species => "Cafadan";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Cafadan";
}
