using System.Collections.Generic;
using System.Linq;

namespace Grauenwolf.TravellerTools.Characters;

public class SkillTemplateCollection<T> : List<T> where T : SkillTemplate
{
    public SkillTemplateCollection(IEnumerable<T> randomSkills) : base(randomSkills)
    {
    }

    public SkillTemplateCollection()
    {
    }

    public T? this[string name] => this.FirstOrDefault(x => x.Name == name);

    public T? this[string name, string? specialty] => this.FirstOrDefault(x => x.Name == name && x.Specialty == specialty);

    public void Remove(string name, string? specialty)
    {
        var skill = this[name, specialty];
        if (skill != null)
            Remove(skill);
    }

    public void Remove(string name)
    {
        var skill = this[name, null];
        if (skill != null)
            Remove(skill);
    }

    /// <summary>
    /// Removes the overlapping skills so you don't pick something that you already have.
    /// </summary>
    /// <param name="skills">The skills.</param>
    /// <param name="level">The level.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void RemoveOverlap(SkillCollection skills, int level)
    {
        if (level == 0)
        {
            for (var i = Count - 1; i >= 0; i--)
                if (skills.Any(s => this[i].Name == s.Name))
                    RemoveAt(i);
        }
        else
        {
            for (var i = Count - 1; i >= 0; i--)
                if (skills.Any(s => this[i].Name == s.Name && this[i].Specialty == s.Specialty && s.Level >= level))
                    RemoveAt(i);
        }
    }
}