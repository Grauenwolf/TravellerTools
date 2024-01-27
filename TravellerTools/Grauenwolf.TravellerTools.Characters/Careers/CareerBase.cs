using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers;

public abstract class CareerBase(string career, string? assignment, SpeciesCharacterBuilder speciesCharacterBuilder)
{
    readonly SpeciesCharacterBuilder m_SpeciesCharacterBuilder = speciesCharacterBuilder;
    public string? Assignment { get; } = assignment;
    public string Career { get; } = career;
    public string Key => Assignment ?? Career;

    public int QualifyDM { get; set; }

    public string? RequiredSkill { get; set; }

    /// <summary>
    /// Gets the assignment. If that is null, returns the career.
    /// </summary>
    public string ShortName => Assignment ?? Career;

    public abstract string? Source { get; }

    internal virtual bool RankCarryover { get; } = false;

    public void AddOneRandomSkill(Character character, Dice dice)
    {
        var skillList = new SkillTemplateCollection(RandomSkills(character));
        skillList.RemoveOverlap(character.Skills, 1);
        if (skillList.Count > 0)
            character.Skills.Add(dice.Choose(skillList), 1);
    }

    /// <summary>
    /// Adds the one skill at level 1.
    /// </summary>
    /// <returns>Returns false if the character already has all of the skills.</returns>
    public bool AddOneSkill(Character character, Dice dice, params string[] skills)
    {
        var skillList = new SkillTemplateCollection();
        foreach (var skill in skills)
        {
            if (skill.Contains("|"))
            {
                var parts = skill.Split('|');
                skillList.Add(parts[0], parts[1]);
            }
            else
                skillList.AddRange(SpecialtiesFor(character, skill));
        }

        skillList.RemoveOverlap(character.Skills, 1);
        if (skillList.Count == 0)
            return false;

        character.Skills.Add(dice.Choose(skillList), 1);
        return true;
    }

    public void IncreaseOneRandomSkill(Character character, Dice dice)
    {
        character.Skills.Increase(dice.Choose(RandomSkills(character)));
    }

    /*
    /// <summary>
    /// Adds a benefit or a one skill at level 1.
    /// </summary>
    /// <returns>Returns false if the character already has all of the skills.</returns>
    public void AddOneSkillOrBenefit(Character character, Dice dice, Action benefiit, params string[] skills)
    {
        var skillList = new SkillTemplateCollection();
        foreach (var skill in skills)
        {
            if (skill.Contains("|"))
            {
                var parts = skill.Split('|');
                skillList.Add(parts[0], parts[1]);
            }
            else
                skillList.AddRange(SpecialtiesFor(character, skill));
        }

        skillList.RemoveOverlap(character.Skills, 1);
        if (skillList.Count == 0 || dice.NextBoolean())
            benefiit();
        else
            character.Skills.Add(dice.Choose(skillList), 1);
    }
    */

    /// <summary>
    /// Increases one skill by 1 level.
    /// </summary>
    public bool IncreaseOneSkill(Character character, Dice dice, params string[] skills)
    {
        var skillList = new SkillTemplateCollection();
        foreach (var skill in skills)
        {
            if (skill.Contains("|"))
            {
                var parts = skill.Split('|');
                skillList.Add(parts[0], parts[1]);
            }
            else
                skillList.AddRange(SpecialtiesFor(character, skill));
        }
        if (skillList.Count == 0)
            return false;

        character.Skills.Increase(dice.Choose(skillList), 1);
        return true;
    }

    public override string ToString()
    {
        if (Assignment == null)
            return Career;
        else
            return $"{Assignment} ({Career})";
    }

    internal virtual decimal MedicalPaymentPercentage(Character character, Dice dice) => 0;

    /// <summary>
    /// This performs the necessary pre-checks such as required skills, then calls OnQualify.
    /// </summary>
    internal bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (RequiredSkill != null)
            if (!character.Skills.Contains(RequiredSkill))
                return false;

        return OnQualify(character, dice, isPrecheck);
    }

    internal abstract void Run(Character character, Dice dice);

    protected void AddBasicSkills(Character character, Dice dice, bool all, string item1, string item2, string item3, string item4, string item5, string? item6)
    {
        var roll = dice.D(item6 == null ? 5 : 6);

        if (all || roll == 1)
            Add(item1);
        if (all || roll == 2)
            Add(item2);
        if (all || roll == 3)
            Add(item3);
        if (all || roll == 4)
            Add(item4);
        if (all || roll == 5)
            Add(item5);
        if ((all || roll == 6) && item6 != null)
            Add(item6);

        void Add(string item)
        {
            switch (item)
            {
                case "Strength":
                case "Str":
                    character.Strength += 1; return;

                case "Dexterity":
                case "Dex":
                    character.Dexterity += 1; return;

                case "Endurance":
                case "End":
                    character.Endurance += 1; return;

                case "Intellect":
                case "Int":
                    character.Intellect += 1; return;

                case "Education":
                case "Edu":
                    character.Education += 1; return;

                case "SS":
                case "Soc":
                case "SocialStanding":
                    character.SocialStanding += 1; return;

                default:
                    //TODO: Check for illegal skills
                    if (item.Contains("|"))
                    {
                        var parts = item.Split('|');
                        character.Skills.Add(parts[0]);
                    }
                    else
                        character.Skills.Add(item);

                    return;
            }
        }
    }

    protected void FixupSkills(Character character, Dice dice) => m_SpeciesCharacterBuilder.FixupSkills(character, dice);

    protected void Increase(Character character, Dice dice, string item1, string item2, string item3, string item4, string item5, string? item6)
    {
    top:
        var roll = dice.D(item6 == null ? 5 : 6);
        var item = roll switch
        {
            1 => item1,
            2 => item2,
            3 => item3,
            4 => item4,
            5 => item5,
            _ => item6!
        };

        switch (item)
        {
            case "Strength":
            case "Str":
                character.Strength += 1; return;

            case "Dexterity":
            case "Dex":
                character.Dexterity += 1; return;

            case "Endurance":
            case "End":
                character.Endurance += 1; return;

            case "Intellect":
            case "Int":
                character.Intellect += 1; return;

            case "Education":
            case "Edu":
                character.Education += 1; return;

            case "SS":
            case "Soc":
            case "SocialStanding":
                character.SocialStanding += 1; return;
            default:
                if (!IncreaseOneSkill(character, dice, item.Split(',')))
                    goto top; //Try again because your race can't have that skill
                return;
        }
    }

    protected void Injury(Character character, Dice dice, bool severe, int age) => m_SpeciesCharacterBuilder.Injury(character, dice, this, severe, age);

    protected void Injury(Character character, Dice dice, int age) => m_SpeciesCharacterBuilder.Injury(character, dice, this, false, age);

    protected void InjuryRollAge(Character character, Dice dice, bool severe) => m_SpeciesCharacterBuilder.Injury(character, dice, this, severe, character.Age + dice.D(4));

    protected void InjuryRollAge(Character character, Dice dice) => m_SpeciesCharacterBuilder.Injury(character, dice, this, false, character.Age + dice.D(4));

    protected void LifeEvent(Character character, Dice dice) => m_SpeciesCharacterBuilder.LifeEvent(character, dice, this);

    /// <summary>
    /// Determines if the specified character qualifies for the career.
    /// </summary>
    /// <param name="isPrecheck">Pretend the character rolled an 8. Used to determine which careers to try.</param>
    protected abstract bool OnQualify(Character character, Dice dice, bool isPrecheck);

    protected void PreCareerEvents(Character character, Dice dice, CareerBase career, SkillTemplateCollection skills) => m_SpeciesCharacterBuilder.PreCareerEvents(character, dice, career, skills);

    protected ImmutableArray<PsionicSkillTemplate> PsionicTalents(Character character) => m_SpeciesCharacterBuilder.Book(character).PsionicTalents;

    /// <summary>
    /// Gets the list of random skills. Skills needing specialization will be excluded. For example, "Art (Performer)" will be included but just "Art" will not.
    /// </summary>
    protected ImmutableArray<SkillTemplate> RandomSkills(Character character) => m_SpeciesCharacterBuilder.Book(character).RandomSkills;

    protected CareerBase RollDraft(Character character, Dice dice) => m_SpeciesCharacterBuilder.RollDraft(character, dice);

    /// <summary>
    /// Returns all the specialities for a skill. If it has no specialities, then just return the skill.
    /// </summary>
    protected List<SkillTemplate> SpecialtiesFor(Character character, string skillName) => m_SpeciesCharacterBuilder.Book(character).SpecialtiesFor(skillName);

    protected void TestPsionic(Character character, Dice dice, int age) => m_SpeciesCharacterBuilder.TestPsionic(character, dice, age);

    protected void UnusualLifeEvent(Character character, Dice dice) => m_SpeciesCharacterBuilder.UnusualLifeEvent(character, dice);
}
