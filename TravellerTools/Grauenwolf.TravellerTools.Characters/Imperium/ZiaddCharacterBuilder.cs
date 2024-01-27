namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class ZiaddCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "High gravity\r\nHirsutism\r\nIncreased strength\r\nOligodactyly\r\nTaint adaptation";
    public override string Species => "Ziadd";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Ziadd";
}