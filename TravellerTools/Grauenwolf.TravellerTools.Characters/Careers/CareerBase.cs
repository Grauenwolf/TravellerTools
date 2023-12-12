using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers;

public abstract class CareerBase
{
    readonly CharacterBuilder m_CharacterBuilder;

    protected CareerBase(string career, string? assignment, CharacterBuilder characterBuilder)
    {
        Career = career;
        Assignment = assignment;
        m_CharacterBuilder = characterBuilder;
    }

    public string? Assignment { get; }

    public string Career { get; }

    public string Key
    {
        get { return Assignment ?? Career; }
    }

    /// <summary>
    /// Gets the assignment. If that is null, returns the career.
    /// </summary>
    public string ShortName => Assignment ?? Career;

    internal Book Book => m_CharacterBuilder.Book;

    protected virtual int QualifyDM => 0;

    protected ImmutableArray<SkillTemplate> RandomSkills
    {
        get { return Book.RandomSkills; }
    }

    public override string ToString()
    {
        if (Assignment == null)
            return Career;
        else
            return $"{Assignment} ({Career})";
    }

    internal virtual decimal MedicalPaymentPercentage(Character character, Dice dice)
    {
        return 0;
    }

    /// <summary>
    /// Qualifies the specified character.
    /// </summary>
    /// <param name="isPrecheck">Pretend the character rolled an 8. Used to determine which careers to try.</param>
    internal abstract bool Qualify(Character character, Dice dice, bool isPrecheck);

    internal abstract void Run(Character character, Dice dice);

    protected void FixupSkills(Character character)
    {
        m_CharacterBuilder.FixupSkills(character);
    }

    protected void Injury(Character character, Dice dice, bool severe, int age) => m_CharacterBuilder.Injury(character, dice, this, severe, age);

    protected void Injury(Character character, Dice dice, int age) => m_CharacterBuilder.Injury(character, dice, this, false, age);

    protected void InjuryRollAge(Character character, Dice dice, bool severe) => m_CharacterBuilder.Injury(character, dice, this, severe, character.Age + dice.D(4));

    protected void InjuryRollAge(Character character, Dice dice) => m_CharacterBuilder.Injury(character, dice, this, false, character.Age + dice.D(4));

    protected void LifeEvent(Character character, Dice dice) => m_CharacterBuilder.LifeEvent(character, dice, this);

    protected void PreCareerEvents(Character character, Dice dice, CareerBase career, SkillTemplateCollection skills) => m_CharacterBuilder.PreCareerEvents(character, dice, career, skills);

    protected CareerBase RollDraft(Dice dice) => m_CharacterBuilder.RollDraft(dice);

    protected List<SkillTemplate> SpecialtiesFor(string skillName) => Book.SpecialtiesFor(skillName);

    protected void TestPsionic(Character character, Dice dice, int age) => m_CharacterBuilder.TestPsionic(character, dice, age);

    protected void UnusualLifeEvent(Character character, Dice dice) => m_CharacterBuilder.UnusualLifeEvent(character, dice);
}
