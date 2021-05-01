using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace Grauenwolf.TravellerTools.Names
{
    public class NameGenerator 
    {
        readonly ImmutableList<string> m_LastNames;
        readonly ImmutableList<string> m_FemaleNames;
        readonly ImmutableList<string> m_MaleNames;

        public NameGenerator(string dataPath)
        {
            var femaleFile = new FileInfo(Path.Combine(dataPath, "female_first.txt"));
            var lastFile = new FileInfo(Path.Combine(dataPath, "last.txt"));
            var maleFile = new FileInfo(Path.Combine(dataPath, "male_first.txt"));

            m_LastNames = File.ReadAllLines(lastFile.FullName).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToImmutableList();
            m_FemaleNames = File.ReadAllLines(femaleFile.FullName).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToImmutableList();
            m_MaleNames = File.ReadAllLines(maleFile.FullName).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToImmutableList();
        }

        public RandomPerson CreateRandomPerson(Dice random, bool? isMale = null)
        {
            isMale ??= random.NextBoolean();

            return new RandomPerson(
                 isMale.Value ? random.Choose(m_MaleNames) : random.Choose(m_FemaleNames),
                 random.Choose(m_LastNames),
                 isMale.Value ? "M" : "F"
                );
        }
    }
}
