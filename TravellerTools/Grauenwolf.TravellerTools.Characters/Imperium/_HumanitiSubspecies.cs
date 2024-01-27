namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class GeoneeCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "High gravity\r\nDense atmosphere adaptation\r\nTechnophilia";
    public override string Species => "Geonee";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Geonee";
}

public class HanenCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Lack of significant body or facial hair\r\nAdapted for aquatic environment";
    public override string Species => "Hanen";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Hanen";
}

public class IltharanCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Water adaptation\r\nCold climate adaptation\r\nNight vision";
    public override string Species => "Iltharan";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Iltharan";
}

public class IrhadreCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Species => "Irhadre";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Irhadre";
}

public class KedepuCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Hegemony of Lorean";
    public override string? Remarks => "Technophile";
    public override string Species => "Kedepu";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Kedepu";
}

public class LancianCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string Species => "Lancian";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Lancian";
}

public class LibertCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Desert adaptation\r\nAridity adaptation\r\nTrace atmosphere adaptation";
    public override string Species => "Libert";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Libert";
}

public class LurianiCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Water adaptation\r\nCold climate adaptation\r\nRepoductive mutation\r\nRespiratory mutation";
    public override string Species => "Luriani";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Luriani";
}

/*
public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

public class CharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/";
}

*/
