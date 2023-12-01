namespace Grauenwolf.TravellerTools.Characters;

public class SkillTemplateCollection : SkillTemplateCollection<SkillTemplate>
{
    public SkillTemplateCollection(IEnumerable<SkillTemplate> randomSkills) : base(randomSkills)
    {
    }

    public SkillTemplateCollection()
    {
    }

    public SkillTemplateCollection(params string[] names)
    {
        if (names == null)
            throw new ArgumentNullException(nameof(names));

        foreach (var name in names)
            Add(name);
    }

    /// <summary>
    /// Adds a skill if it doesn't already exist.
    /// </summary>
    /// <param name="name">The name.</param>
    public void Add(string name)
    {
        var skill = this[name];
        if (skill == null)
            Add(new SkillTemplate(name));
    }

    /// <summary>
    /// Adds a skill if it doesn't already exist with the indicated speciality.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="specialty">The specialty.</param>
    public void Add(string name, string specialty)
    {
        var skill = this[name, specialty];
        if (skill == null)
            Add(new SkillTemplate(name, specialty));
    }

    public void AddRange(IEnumerable<string> skills)
    {
        if (skills == null)
            throw new ArgumentNullException(nameof(skills), $"{nameof(skills)} is null.");

        foreach (var skill in skills)
            Add(skill);
    }

    public void AddRange(string name, IEnumerable<string> specialties)
    {
        if (specialties == null)
            throw new System.ArgumentNullException(nameof(specialties));

        foreach (var specialty in specialties)
            Add(name, specialty);
    }
}
