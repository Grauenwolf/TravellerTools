namespace Grauenwolf.TravellerTools.Characters;

public class SkillCollection : List<Skill>
{
    public Skill? this[string name] => this.FirstOrDefault(x => x.Name == name);

    public Skill? this[string name, string? specialty] => this.FirstOrDefault(x => x.Name == name && x.Specialty == specialty);

    /// <summary>
    /// Adds a skill if it doesn't already exist with the indicated minimum level.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="minLevel">The minimum level.</param>
    public void Add(string name, int minLevel = 0, bool isPsionicTalent = false)
    {
        var skill = this[name];
        if (skill == null)
            Add(new Skill(name, minLevel) { IsPsionicTalent = isPsionicTalent });
        else
            skill.Level = Math.Max(skill.Level, minLevel);
    }

    /// <summary>
    /// Adds a skill if it doesn't already exist with the indicated minimum level.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="minLevel">The minimum level.</param>
    public void Add(string name, string? specialty, int minLevel = 0, bool isPsionicTalent = false)
    {
        var skill = this[name, specialty];
        if (skill == null)
            Add(new Skill(name, specialty, minLevel) { IsPsionicTalent = isPsionicTalent });
        else
            skill.Level = Math.Max(skill.Level, minLevel);
    }

    public void Add(SkillTemplate skill, int minLevel = 0)
    {
        if (skill == null)
            throw new ArgumentNullException(nameof(skill), $"{nameof(skill)} is null.");

        Add(skill.Name, skill.Specialty, minLevel, skill is PsionicSkillTemplate);
    }

    public void AddRange(List<SkillTemplate> skillList, int minLevel = 0)
    {
        if (skillList == null)
            throw new ArgumentNullException(nameof(skillList));

        foreach (var skill in skillList)
            Add(skill, minLevel);
    }

    /// <summary>
    /// Gets the best the skill from the list. If there are ties, choose the first one.
    /// </summary>
    /// <param name="skillNames">The skill names or specialities.</param>
    public Skill? BestSkill()
    {
        Skill? bestSkill = null;
        var bestScore = -3; //unskilled penalty
        foreach (var skill in this)
            if (skill.Level > bestScore)
            {
                bestScore = skill.Level;
                bestSkill = skill;
            }

        return bestSkill;
    }

    /// <summary>
    /// Gets the best the skill from the list. If there are ties, choose the first one. Jack-of-All-Trades is not allowed.
    /// </summary>
    /// <param name="skillNames">The skill names or specialities.</param>
    public Skill? BestSkill(params string[] skillNames)
    {
        if (skillNames == null || skillNames.Length == 0)
            return BestSkill();

        Skill? bestSkill = null;
        var bestScore = -3; //unskilled penalty
        foreach (var skill in this)
            foreach (var name in skillNames)
                if (((skill.Name == name) || (skill.Specialty == name)) && (skill.Level > bestScore))
                {
                    bestScore = skill.Level;
                    bestSkill = skill;
                }

        return bestSkill;
    }

    /// <summary>
    /// Gets the best the skill level from the list.
    /// </summary>
    /// <param name="skillNames">The skill names or specialities.</param>
    public int BestSkillLevel(params string[] skillNames)
    {
        if (skillNames == null || skillNames.Length == 0)
            throw new ArgumentException($"{nameof(skillNames)} is null or empty.", nameof(skillNames));

        var bestScore = -3; //unskilled penalty
        foreach (var skill in this)
        {
            foreach (var name in skillNames)
                if (skill.Name == name || skill.Specialty == name)
                    bestScore = Math.Max(bestScore, skill.Level);

            if (skill.Name == "Jack-of-All-Trades" && bestScore < 0)
                bestScore = skill.Level - 3;
        }

        return bestScore;
    }

    public void Collapse()
    {
        //Remove level 0 skills if there are other skills with the same name
        var tempList = this.Where(s => s.Level == 0).ToList();
        foreach (var skill in tempList)
            if (this.Any(s => s != skill && s.Name == skill.Name))
                this.Remove(skill);
    }

    public bool Contains(string name) => this.Any(s => s.Name == name);

    public int EffectiveSkillLevel(string name, string? specialty = null)
    {
        var skill = this[name, specialty];
        if (skill != null)
            return skill.Level;

        var joat = this["Jack-of-All-Trades"];
        if (joat != null)
            return -3 + joat.Level;
        else
            return -3;
    }

    public int GetLevel(string name, string? specialty = null)
    {
        var skill = this[name, specialty];
        if (skill != null)
            return skill.Level;

        skill = this["Jack-of-All-Trades"];
        if (skill != null)
            return skill.Level - 3;

        return -3;
    }

    /// <summary>
    /// Adds or improves the indicated skill.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="levels">The levels.</param>
    public void Increase(string name, int levels = 1)
    {
        var skill = this[name, null];
        if (skill == null)
            Add(new Skill(name, levels));
        else
            skill.Level += levels;
    }

    /// <summary>
    /// Adds or improves the indicated skill.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="levels">The levels.</param>
    public void Increase(string name, string? specialty, int levels = 1)
    {
        var skill = this[name, specialty];
        if (skill == null)
            Add(new Skill(name, specialty, levels));
        else
            skill.Level += levels;
    }

    public void Increase(SkillTemplate skill, int levels = 1)
    {
        if (skill == null)
            throw new ArgumentNullException(nameof(skill), $"{nameof(skill)} is null.");

        Increase(skill.Name, skill.Specialty, levels);
    }

    public void Remove(string name, string specialty)
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
}
