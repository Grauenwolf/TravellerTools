using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Names;

public class NameGenerator
{
    readonly ImmutableArray<string> m_CompanyFirstNames;
    readonly ImmutableArray<string> m_CompanyLastNames;
    readonly ImmutableArray<string> m_FemaleNames;
    readonly ImmutableArray<string> m_LastNames;
    readonly ImmutableArray<string> m_MaleNames;
    readonly ImmutableArray<string> m_MegacorpNames;

    public NameGenerator(string dataPath)
    {
        ImmutableArray<string> LoadFile(string fileName)
        {
            return File.ReadAllLines(new FileInfo(Path.Combine(dataPath, fileName)).FullName).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToImmutableArray();
        }

        m_FemaleNames = LoadFile("female_first.txt");
        m_LastNames = LoadFile("last.txt");
        m_MaleNames = LoadFile("male_first.txt");

        m_CompanyFirstNames = LoadFile("company_first.txt");
        m_CompanyLastNames = LoadFile("company_last.txt");
        m_MegacorpNames = LoadFile("megacorp.txt");
    }

    public string CreateCompanyName(Dice dice)
    {
        if (dice.D(20) == 1)
            return dice.Choose(m_MegacorpNames);

        return dice.Choose(m_CompanyFirstNames) + " " + dice.Choose(m_CompanyLastNames);
    }

    public RandomPerson CreateRandomPerson(Dice dice, bool? isMale = null)
    {
        isMale ??= dice.NextBoolean();

        return new RandomPerson(
             isMale.Value ? dice.Choose(m_MaleNames) : dice.Choose(m_FemaleNames),
             dice.Choose(m_LastNames),
             isMale.Value ? "M" : "F"
            );
    }

    public bool IsMegacorp(string name) => m_MegacorpNames.Contains(name);
}
