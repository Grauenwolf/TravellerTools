namespace Grauenwolf.TravellerTools.Characters.Careers;

abstract class FullCareer : CareerBase
{
    protected FullCareer(string name, string assignment, CharacterBuilder characterBuilder) : base(name, assignment, characterBuilder)
    {
    }

    protected abstract int AdvancedEductionMin { get; }
    protected abstract string AdvancementAttribute { get; }
    protected abstract int AdvancementTarget { get; }
    protected abstract string SurvivalAttribute { get; }
    protected abstract int SurvivalTarget { get; }

    internal abstract void AssignmentSkills(Character character, Dice dice);

    internal abstract void BasicTrainingSkills(Character character, Dice dice, bool all);

    internal abstract void Event(Character character, Dice dice);

    internal abstract void Mishap(Character character, Dice dice, int age);

    internal void MishapRollAge(Character character, Dice dice)
    {
        Mishap(character, dice, character.Age + dice.D(4));
    }

    internal abstract void ServiceSkill(Character character, Dice dice);

    internal abstract void TitleTable(Character character, CareerHistory careerHistory, Dice dice);

    protected abstract void AdvancedEducation(Character character, Dice dice);

    protected void ChangeAssignment(Character character, Dice dice, CareerHistory careerHistory)
    {
        var historyMessage = $"Switched to {careerHistory.LongName}";

        var oldTitle = character.Title;
        TitleTable(character, careerHistory, dice);
        var newTitle = careerHistory.Title;
        if (oldTitle != newTitle && newTitle != null)
        {
            historyMessage += $" with the new title {newTitle}";
            character.Title = newTitle;
        }
        historyMessage += ".";
        character.AddHistory(historyMessage, character.Age);
    }

    protected void ChangeCareer(Character character, Dice dice, CareerHistory careerHistory)
    {
        var historyMessage = $"Became a {careerHistory.LongName}";

        var oldTitle = character.Title;
        TitleTable(character, careerHistory, dice);
        var newTitle = careerHistory.Title;
        if (oldTitle != newTitle && newTitle != null)
        {
            historyMessage += $" with the new title {newTitle}";
            character.Title = newTitle;
        }
        historyMessage += ".";
        character.AddHistory(historyMessage, character.Age);
    }

    protected void Comissioned(Character character, Dice dice, CareerHistory careerHistory)
    {
        careerHistory.CommissionRank = 1;
        var historyMessage = $"Commissioned in {careerHistory.LongName}";

        var oldTitle = character.Title;
        TitleTable(character, careerHistory, dice);
        var newTitle = careerHistory.Title;
        if (oldTitle != newTitle && newTitle != null)
        {
            historyMessage += $" with the new title {newTitle}";
            character.Title = newTitle;
        }
        historyMessage += ".";
        character.AddHistory(historyMessage, character.Age);
    }

    protected abstract void PersonalDevelopment(Character character, Dice dice);

    protected void Promote(Character character, Dice dice, CareerHistory careerHistory, int? age = null)
    {
        string historyMessage;
        if (careerHistory.CommissionRank > 0)
        {
            careerHistory.CommissionRank += 1;
            historyMessage = $"Promoted to {careerHistory.LongName} officer rank {careerHistory.CommissionRank}";
        }
        else
        {
            careerHistory.Rank += 1;
            historyMessage = $"Promoted to {careerHistory.LongName} rank {careerHistory.Rank}";
        }

        var oldTitle = character.Title;
        TitleTable(character, careerHistory, dice);
        var newTitle = careerHistory.Title;
        if (oldTitle != newTitle && newTitle != null)
        {
            historyMessage += $" with the new title {newTitle}";
            character.Title = newTitle;
        }
        historyMessage += ".";
        character.AddHistory(historyMessage, age ?? character.Age);
    }

    protected void UpdateTitle(Character character, Dice dice, CareerHistory careerHistory)
    {
        var oldTitle = character.Title;
        TitleTable(character, careerHistory, dice);
        var newTitle = careerHistory.Title;
        if (oldTitle != newTitle && newTitle != null)
        {
            character.AddHistory($"Is now a {newTitle}.", character.Age);
            character.Title = newTitle;
        }
    }
}
