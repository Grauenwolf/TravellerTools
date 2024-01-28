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
            return File.ReadAllLines(new FileInfo(Path.Combine(dataPath, fileName)).FullName)
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => char.IsLower(x[0]) ? char.ToUpperInvariant(x[0]) + x[1..] : x)
                .Distinct()
                .ToImmutableArray();
        }

        m_FemaleNames = LoadFile("female_first.txt");
        m_LastNames = LoadFile("last.txt");
        m_MaleNames = LoadFile("male_first.txt");

        m_CompanyFirstNames = LoadFile("company_first.txt");
        m_CompanyLastNames = LoadFile("company_last.txt");
        m_MegacorpNames = LoadFile("megacorp.txt");
    }

    public static string AslanName(Dice dice, string genderCode)
    {
        var nm1 = new[] { "", "", "", "", "", "b", "bg", "br", "c", "d", "dh", "g", "h", "j", "k", "ks", "m", "n", "p", "pr", "r", "s", "sh", "t", "v", "y" };
        var nm2 = new[] { "a", "i", "e", "o", "u", "a", "a", "a", "u", "u" };
        var nm3 = new[] { "b", "bh", "bhr", "c", "dr", "gn", "h", "hm", "j", "jn", "k", "kr", "l", "lg", "lm", "m", "n", "nd", "r", "rg", "rm", "rp", "s", "shm", "sk", "sv", "t", "th", "tt", "v" };
        var nm4 = new[] { "b", "bh", "d", "g", "h", "k", "n", "ng", "ngh", "pt", "rh", "rm", "rt", "sh", "shr", "sth", "sv", "t", "thy", "ty", "v", "vy", "y" };
        var nm5 = new[] { "", "", "d", "n", "nt", "r", "s", "sh", "t", "y" };
        var nm6 = new[] { "", "", "", "", "", "", "b", "bh", "c", "d", "dh", "g", "h", "k", "kh", "l", "m", "n", "p", "pr", "r", "s", "sh", "v", "y" };
        var nm7 = new[] { "a", "e", "i", "o", "u", "a", "e", "i", "u", "a", "a", "a", "a", "i", "i" };
        var nm8 = new[] { "b", "bh", "bj", "d", "dh", "dhr", "dr", "dv", "h", "j", "ks", "l", "ly", "m", "mr", "n", "nd", "ng", "nt", "p", "pt", "rg", "rk", "rm", "ry", "s", "sh", "sm", "t", "th", "tr", "tt", "v", "vh" };
        var nm9 = new[] { "bh", "c", "cy", "d", "dh", "dr", "dv", "j", "k", "ks", "l", "ly", "m", "mb", "n", "nd", "ndh", "ng", "nt", "p", "pt", "r", "rg", "rm", "s", "sh", "sm", "sn", "t", "th", "tr", "ts", "tt", "v", "y" };

        string result;
        if (genderCode == "M")
        {
            var i = dice.Next(0, 10);

            var rnd = dice.Choose(nm1);
            var rnd2 = dice.Choose(nm2);
            var rnd3 = dice.Choose(nm3);
            var rnd4 = dice.Choose(nm2);
            var rnd5 = dice.Choose(nm5);
            while (rnd3 == rnd || rnd3 == rnd5)
            {
                rnd3 = dice.Choose(nm3);
            }
            if (i < 4)
            {
                result = rnd + rnd2 + rnd3 + rnd4 + rnd5;
            }
            else
            {
                var rnd6 = dice.Choose(nm2);
                var rnd7 = dice.Choose(nm4);
                while (rnd7 == rnd3 || rnd7 == rnd5)
                {
                    rnd7 = dice.Choose(nm4);
                }
                if (i < 8)
                {
                    result = rnd + rnd2 + rnd3 + rnd4 + rnd7 + rnd6 + rnd5;
                }
                else
                {
                    var rnd8 = dice.Choose(nm2);
                    var rnd9 = dice.Choose(nm4);
                    while (rnd7 == rnd9 || rnd9 == rnd5)
                    {
                        rnd9 = dice.Choose(nm4);
                    }
                    result = rnd + rnd2 + rnd3 + rnd4 + rnd7 + rnd6 + rnd9 + rnd8 + rnd5;
                }
            }
        }
        else
        {
            var i = dice.Next(0, 10);
            var rnd = dice.Choose(nm6);
            var rnd2 = dice.Choose(nm7);
            var rnd3 = dice.Choose(nm8);
            var rnd4 = dice.Choose(nm7);
            if (i < 4)
            {
                result = rnd + rnd2 + rnd3 + rnd4;
            }
            else
            {
                var rnd6 = dice.Choose(nm7);
                var rnd7 = dice.Choose(nm9);
                while (rnd7 == rnd3)
                {
                    rnd7 = dice.Choose(nm9);
                }

                if (i < 8)
                {
                    result = rnd + rnd2 + rnd3 + rnd4 + rnd7 + rnd6;
                }
                else
                {
                    var rnd8 = dice.Choose(nm7);
                    var rnd9 = dice.Choose(nm9);
                    while (rnd7 == rnd9)
                    {
                        rnd9 = dice.Choose(nm4);
                    }
                    result = rnd + rnd2 + rnd3 + rnd4 + rnd7 + rnd6 + rnd9 + rnd8;
                }
            }
        }

        return char.ToUpperInvariant(result[0]) + result[1..];
    }

    public string CreateCompanyName(Dice dice)
    {
        if (dice.D(20) == 1)
            return dice.Choose(m_MegacorpNames);

        return dice.Choose(m_CompanyFirstNames) + " " + dice.Choose(m_CompanyLastNames);
    }

    [Obsolete("Use the species character generator to create genders and names.")]
    public RandomPerson CreateRandomPerson(Dice dice, bool? isMale = null)
    {
        isMale ??= dice.NextBoolean();

        return new RandomPerson(
             isMale.Value ? dice.Choose(m_MaleNames) : dice.Choose(m_FemaleNames),
             dice.Choose(m_LastNames),
             isMale.Value ? "M" : "F"
            );
    }

    public string HumanitiName(Dice dice, string genderCode)
    {
        if (genderCode == "M")
            return dice.Choose(m_MaleNames) + " " + dice.Choose(m_LastNames);
        else
            return dice.Choose(m_FemaleNames) + " " + dice.Choose(m_LastNames);
    }

    public bool IsMegacorp(string name) => m_MegacorpNames.Contains(name);
}