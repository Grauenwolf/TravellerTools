using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Characters;

public class PsionicSkillTemplateCollection : SkillTemplateCollection<PsionicSkillTemplate>
{
    public PsionicSkillTemplateCollection(IEnumerable<PsionicSkillTemplate> randomSkills) : base(randomSkills)
    {
    }

    public PsionicSkillTemplateCollection()
    {
    }

    /// <summary>
    /// Adds a skill if it doesn't already exist.
    /// </summary>
    /// <param name="name">The name.</param>
    public void Add(string name, int learningDM)
    {
        var skill = this[name];
        if (skill == null)
            Add(new PsionicSkillTemplate(name, learningDM));
    }
}