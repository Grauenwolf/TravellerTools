using Grauenwolf.TravellerTools.Names;

namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class XXCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "";
}

/*

public class CharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "";
}

public class CharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "";
}

public class CharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "";
}

public class CharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override string? Remarks => "";
    public override string Species => "";
    public override string SpeciesUrl => "";
}

*/
