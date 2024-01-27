using Grauenwolf.TravellerTools.Names;

namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class AnswerinCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override string? Remarks => "Warrior race\r\nDigestive mutation\r\nHormonal mutation";
    public override string Species => "Answerin";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Answerin";
}

public class AyanshiCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override string? Remarks => "Long lifespan\r\nNight vision\r\nReproductive mutation";
    public override string Species => "Ayansh'i";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Ayansh%27i";
}

public class AzhantiCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override string? Remarks => "Shorter lifespan\r\nDense atmosphere adaptation\r\nHigh gravity\r\nHot climate adaptation\r\nIncreased strength\r\nTaint adaptation";
    public override string Species => "Azhanti";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Azhanti";
}

public class CafadanCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override string? Remarks => "Shorter lifespans\r\nCold climate adaptation\r\nNight vision\r\nTroglobytic adaptation";
    public override string Species => "Cafadan";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Cafadan";
}

public class CassilldanCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override string? Remarks => "Low gravity\r\nGreater height\r\nTaint adaptation";
    public override string Species => "Cassilldan";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Cassilldan";
}

public class DarrianImperiumCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override string? Remarks => "Daryen\r\nLow gravity\r\nCold climate adaptation\r\nDigestive mutation\r\nRadiation adaptation\r\nTechnophilia";
    public override string Species => "Darrian, Imperium";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Daryen";

    protected override void InitialCharacterStats(Dice dice, Character character)
    {
        base.InitialCharacterStats(dice, character);
        character.Strength += -1;
        character.Dexterity += 1;
        character.Endurance += -1;
        character.Intellect += 1;
        character.Education += 1;
    }
}

public class SolomaniImperiumCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override string Species => "Solomani, Imperium";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Solomani";
}

public class VilaniImperiumCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override string Species => "Vilani, Imperium";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Vilani";
}

public class ZhodaniImperiumCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override string Species => "Zhodani, Imperium";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Zhodani";
}

public class DarmineCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : ImperiumCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override string? Remarks => "Dense atmosphere adaptation\r\nRadiation resistance\r\nTaint adaptation";
    public override string Species => "Darmine";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Darmine_(race)";
}
