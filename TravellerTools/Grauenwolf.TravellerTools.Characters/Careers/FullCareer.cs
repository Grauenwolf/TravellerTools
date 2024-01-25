namespace Grauenwolf.TravellerTools.Characters.Careers;

abstract class FullCareer(string name, string? assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : CareerBase(name, assignment, speciesCharacterBuilder)
{
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

    protected void ChangeAssignment(Character character, Dice dice, CareerHistory careerHistory, bool rankCarryover)
    {
        var historyMessage = $"Switched to {careerHistory.LongName}";

        var oldTitle = character.Title;
        TitleTable(character, careerHistory, dice);
        var newTitle = careerHistory.Title;
        if (oldTitle != newTitle && newTitle != null)
        {
            historyMessage += $" with the new title {newTitle}";
            character.Title = newTitle;

            if (rankCarryover)
                historyMessage += $" and previous rank";
        }
        else if (rankCarryover)
            historyMessage += $" with previous rank";

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

    protected CareerHistory NextTermSetup(Character character, Dice dice)
    {
        CareerHistory careerHistory;
        if (!character.CareerHistory.Any(pc => pc.Career == Career))
        {
            careerHistory = new CareerHistory(character.Age, Career, Assignment, 0);
            ChangeCareer(character, dice, careerHistory);
            BasicTrainingSkills(character, dice, character.CareerHistory.Count == 0);
            FixupSkills(character, dice);

            character.CareerHistory.Add(careerHistory); //add this after basic training so the prior count isn't wrong.
        }
        else
        {
            var basicTraining = false;
            if (!character.CareerHistory.Any(pc => pc.Assignment == Assignment))
            {
                if (RankCarryover && character.CareerHistory.Any(pc => pc.Career == Career))
                {
                    //copy old rank
                    var rank = character.CareerHistory.Where(pc => pc.Career == Career).Max(pc => pc.Rank);
                    var commissionRank = character.CareerHistory.Where(pc => pc.Career == Career).Max(pc => pc.CommissionRank);
                    careerHistory = new CareerHistory(character.Age, Career, Assignment, rank, commissionRank);
                    character.CareerHistory.Add(careerHistory);
                    ChangeAssignment(character, dice, careerHistory, true);
                }
                else
                {
                    //learn basic training
                    careerHistory = new CareerHistory(character.Age, Career, Assignment, 0);
                    character.CareerHistory.Add(careerHistory);
                    ChangeAssignment(character, dice, careerHistory, false);
                    BasicTrainingSkills(character, dice, false);
                    basicTraining = true;
                }
                FixupSkills(character, dice);
            }
            else if (character.LastCareer?.Assignment == Assignment)
            {
                careerHistory = character.CareerHistory.Single(pc => pc.Assignment == Assignment);
                character.AddHistory($"Continued as {careerHistory.LongName}.", character.Age);
                careerHistory.LastTermAge = character.Age;
            }
            else
            {
                careerHistory = character.CareerHistory.Single(pc => pc.Assignment == Assignment);
                character.AddHistory($"Returned to {careerHistory.LongName}.", character.Age);
                careerHistory.LastTermAge = character.Age;
            }

            if (!basicTraining)
            {
                var skillTables = new List<SkillTable>();
                skillTables.Add(PersonalDevelopment);
                skillTables.Add(ServiceSkill);
                skillTables.Add(AssignmentSkills);
                if (character.Education >= AdvancedEductionMin)
                    skillTables.Add(AdvancedEducation);

                dice.Choose(skillTables)(character, dice);
                FixupSkills(character, dice);
            }
        }

        return careerHistory;
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
