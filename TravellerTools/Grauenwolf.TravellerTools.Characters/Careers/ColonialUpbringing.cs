using System.Linq;

namespace Grauenwolf.TravellerTools.Characters.Careers;

class ColonialUpbringing(Book book) : CareerBase("Colonial Upbringing", null, book)
{
    internal override bool Qualify(Character character, Dice dice)
    {
        if (character.CurrentTerm != 1)
            return false;

        return true;
    }

    internal override void Run(Character character, Dice dice)
    {
        character.AddHistory($"Entered Colonial Upbringing.", character.Age);

        character.LongTermBenefits.CommissionDM = -1;
        character.LongTermBenefits.AdvancementDM = -1;
        character.LongTermBenefits.QualificationDM = -2;
        character.LongTermBenefits.EnlistmentDM.Add("Rogue", 3); //This is a net +1 after QualificationDM
        character.LongTermBenefits.EnlistmentDM.Add("Scout", 3); //This is a net +1 after QualificationDM

        character.Skills.Add("Animals");
        character.Skills.Add("Athletics");
        character.Skills.Add("Drive");
        character.Skills.Add("Gun Combat");
        character.Skills.Add("Mechanic");
        character.Skills.Add("Medic");
        character.Skills.Add("Navigation");
        character.Skills.Add("Recon");
        character.Skills.Add("Profession");
        character.Skills.Add("Seafarer");
        character.Skills.Add("Survival", 1);

        var graduation = dice.D(2, 6) + character.IntellectDM + character.CurrentTermBenefits.GraduationDM;
        if (character.Endurance >= 8)
            graduation += 1;

        character.Age = 22 + dice.D(2, 3);
        character.EducationHistory = new EducationHistory();
        character.EducationHistory.Name = "Colonial Upbringing";

        if (graduation < 8)
        {
            character.AddHistory($"Failed as a colonist.", character.Age);
            character.EducationHistory.Status = "Failed";
        }
        else
        {
            IncreaseLevel0Skill(character, dice);

            if (dice.NextBoolean()) //Pick 2 more skills
            {
                IncreaseLevel0Skill(character, dice);
                IncreaseLevel0Skill(character, dice);
            }
            else //Increase 1 skill that was already at 1+
            {
                var skillChoices2 = character.Skills.Where(s => s.Level > 0).ToList();
                dice.Choose(skillChoices2).Level += 1;
            }

            character.Skills.Add("Jack-of-All-Trades", 1);

            if (graduation >= 12)
            {
                character.AddHistory($"Left the colonies with honors.", character.Age);
                character.Skills.Add("Leadership", 1);
                IncreaseLevel0Skill(character, dice);
                character.EducationHistory.Status = "Honors";
            }
            else
            {
                character.EducationHistory.Status = "Completed";
                character.AddHistory($"Left the colonies.", character.Age);
            }

            character.Endurance += 1;
            character.Education -= dice.D(3);
        }
    }

    private void IncreaseLevel0Skill(Character character, Dice dice)
    {
        var skillChoices = new SkillTemplateCollection();
        foreach (var skill in character.Skills.Where(s => s.Level == 0))
            skillChoices.AddRange(SpecialtiesFor(skill.Name));

        //Remove skills we already have at level 1
        skillChoices.RemoveOverlap(character.Skills, 1);
        character.Skills.Add(dice.Pick(skillChoices), 1);
    }
}
