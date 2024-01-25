using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters;

public class Book
{
    readonly CharacterTemplates m_Templates;

    internal Book(CharacterTemplates templates)
    {
        m_Templates = templates;

        var skills = new List<SkillTemplate>();
        var allSkills = new List<SkillTemplate>();
        foreach (var skill in m_Templates.Skills)
        {
            if (skill.Name == "Jack-of-All-Trades")
                continue;

            allSkills.Add(new SkillTemplate(skill.Name));

            if (skill.Specialty?.Length > 0)
                foreach (var specialty in skill.Specialty)
                {
                    var template = new SkillTemplate(skill.Name, specialty.Name, specialty.Group);
                    skills.Add(template);
                    allSkills.Add(template);
                }
            else
                skills.Add(new SkillTemplate(skill.Name));
        }
        RandomSkills = ImmutableArray.CreateRange(skills);
        AllSkills = ImmutableArray.CreateRange(allSkills);

        PsionicTalents = ImmutableArray.Create(
            new PsionicSkillTemplate("Telepathy", 4),
            new PsionicSkillTemplate("Clairvoyance", 3),
            new PsionicSkillTemplate("Telekinesis", 2),
            new PsionicSkillTemplate("Awareness", 1),
            new PsionicSkillTemplate("Teleportation", 0)
            );
    }

    /// <summary>
    /// Gets all of the skills, including unspecialized skills. For example, both "Art" and "Art (Performer)" will be included.
    /// </summary>
    /// <value>All skills.</value>
    public ImmutableArray<SkillTemplate> AllSkills { get; }

    public ImmutableArray<PsionicSkillTemplate> PsionicTalents { get; }

    /// <summary>
    /// Gets the list of random skills. Skills needing specialization will be excluded. For example, "Art (Performer)" will be included but just "Art" will not.
    /// </summary>
    public ImmutableArray<SkillTemplate> RandomSkills { get; }

    public bool RequiresSpeciality(string name)
    {
        return RandomSkills.Any(s => s.Name == name && s.Specialty != null);
    }

    internal List<SkillTemplate> SpecialtiesFor(string skillName)
    {
        var skill = m_Templates.Skills.FirstOrDefault(s => s.Name == skillName);
        if (skill != null && skill.Specialty != null)
            return skill.Specialty.Select(s => new SkillTemplate(skillName, s.Name)).ToList();
        else
            return new List<SkillTemplate>() { new SkillTemplate(skillName) };
    }
}
