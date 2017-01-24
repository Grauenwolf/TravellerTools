namespace Grauenwolf.TravellerTools.Characters.Careers
{
    abstract class FullCareer : Career
    {
        protected FullCareer(string name, string assignment) : base(name, assignment)
        {
        }

        protected abstract int AdvancedEductionMin { get; }
        protected abstract string AdvancementAttribute { get; }
        protected abstract int AdvancementTarget { get; }
        protected abstract string SurvivalAttribute { get; }
        protected abstract int SurvivalTarget { get; }

        internal abstract void BasicTrainingSkills(Character character, Dice dice, bool all);

        internal abstract void AssignmentSkills(Character character, Dice dice);

        internal abstract void Event(Character character, Dice dice);

        internal abstract void Mishap(Character character, Dice dice);

        internal abstract void TitleTable(Character character, CareerHistory careerHistory, Dice dice);

        protected abstract void AdvancedEducation(Character character, Dice dice);

        protected abstract void PersonalDevelopment(Character character, Dice dice);

        protected abstract void ServiceSkill(Character character, Dice dice);

        protected void UpdateTitle(Character character, Dice dice, CareerHistory careerHistory)
        {
            var oldTitle = character.Title;
            TitleTable(character, careerHistory, dice);
            var newTitle = careerHistory.Title;
            if (oldTitle != newTitle && newTitle != null)
            {
                character.AddHistory($"Is now a {newTitle}");
                character.Title = newTitle;
            }
        }
    }
}

