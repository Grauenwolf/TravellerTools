namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class LurianiCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Water adaptation\r\nCold climate adaptation\r\nRepoductive mutation\r\nRespiratory mutation";
    public override string Species => "Luriani";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Luriani";
}
