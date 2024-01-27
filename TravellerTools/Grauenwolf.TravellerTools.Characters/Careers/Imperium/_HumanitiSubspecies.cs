namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class GeoneeCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "High gravity\r\nDense atmosphere adaptation\r\nTechnophilia";
    public override string Species => "Geonee";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Geonee";
}

public class HanenCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Lack of significant body or facial hair\r\nAdapted for aquatic environment";
    public override string Species => "Hanen";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Hanen";
}

public class IltharanCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Water adaptation\r\nCold climate adaptation\r\nNight vision";
    public override string Species => "Iltharan";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Iltharan";
}

public class IrhadreCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Species => "Irhadre";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Irhadre";
}

/*
public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}
public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}
public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

*/
