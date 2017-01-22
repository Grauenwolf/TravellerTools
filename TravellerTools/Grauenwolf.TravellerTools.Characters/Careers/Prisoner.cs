using Grauenwolf.TravellerTools;
using System.Collections.Generic;
using System.Linq;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
    abstract class Prisoner : Career
    {
        protected Prisoner(string assignment) : base("Prisoner", assignment)
        {
        }

        protected abstract string AdvancementAttribute { get; }
        protected abstract int AdvancementTarget { get; }
        protected abstract string SurvivalAttribute { get; }
        protected abstract int SurvivalTarget { get; }
        public override bool Qualify(Character character, Dice dice)
        {
            return false;
        }

        public override void Run(Character character, Dice dice)
        {
            CareerHistory careerHistory;
            if (!character.CareerHistory.Any(pc => pc.Name == Name))
            {
                character.AddHistory($"Became a {Assignment} at age {character.Age}");
                BasicTraining(character, dice, character.CareerHistory.Count == 0);

                careerHistory = new CareerHistory(Name, Assignment, 0);
                character.CareerHistory.Add(careerHistory);
            }
            else
            {
                if (!character.CareerHistory.Any(pc => pc.Assignment == Assignment))
                {
                    character.AddHistory($"Switched to {Assignment} at age {character.Age}");
                    careerHistory = new CareerHistory(Name, Assignment, 0); //TODO: Carry-over rank?
                    character.CareerHistory.Add(careerHistory);
                }
                else if (character.LastCareer?.Assignment == Assignment)
                {
                    character.AddHistory($"Continued as {Assignment} at age {character.Age}");
                    careerHistory = character.CareerHistory.Single(pc => pc.Assignment == Assignment);
                }
                else
                {
                    character.AddHistory($"Returned to {Assignment} at age {character.Age}");
                    careerHistory = character.CareerHistory.Single(pc => pc.Assignment == Assignment);
                }

                var skillTables = new List<SkillTable>();
                skillTables.Add(PersonalDevelopment);
                skillTables.Add(ServiceSkill);
                skillTables.Add(AssignmentSkills);


                dice.Choose(skillTables)(character, dice, dice.D(1, 6), false);
            }
            careerHistory.Terms += 1;

            if (character.Parole == null)
                character.Parole = dice.D(6) + 4;

            var survived = dice.RollHigh(character.GetDM(SurvivalAttribute) + character.NextTermBenefits.SurvivalDM, SurvivalTarget);
            if (survived)
            {
                character.BenefitRolls += 1;

                Event(character, dice);

                var advancementRoll = dice.D(2, 6);

                advancementRoll += character.GetDM(AdvancementAttribute) + character.CurrentTermBenefits.AdvancementDM;

                if (advancementRoll > AdvancementTarget)
                {

                    careerHistory.Rank += 1;
                    character.AddHistory($"Promoted to rank {careerHistory.Rank}");

                    UpdateTitle(character, careerHistory, dice);


                    //advancement skill
                    var skillTables = new List<SkillTable>();
                    skillTables.Add(PersonalDevelopment);
                    skillTables.Add(ServiceSkill);
                    skillTables.Add(AssignmentSkills);
                    dice.Choose(skillTables)(character, dice, dice.D(1, 6), false);
                }

                if (character.NextTermBenefits.MusterOut)
                {
                    //must have escaped due to event roll.
                }
                else if (advancementRoll > character.Parole)
                {
                    character.NextTermBenefits.MusterOut = true;
                    character.AddHistory("Paroled from prison.");
                }
                else
                    character.NextTermBenefits.MustEnroll = "Prisoner";
            }
            else
            {
                character.NextTermBenefits.MustEnroll = "Prisoner";
                Mishap(character, dice);
            }

            character.LastCareer = careerHistory;
            character.Age += 4;
        }

        internal void Event(Character character, Dice dice)
        {
            switch (dice.D(2, 6))
            {
                case 2:
                    Mishap(character, dice);
                    return;
                case 3:
                    if (dice.RollHigh(character.Skills.BestSkillLevel("Stealth", "Deception"), 10))
                    {
                        character.AddHistory("Escaped from prison.");
                        character.NextTermBenefits.MusterOut = true;
                    }
                    else
                    {
                        character.AddHistory("Failed to escaped from prison.");
                        character.Parole += 2;
                    }
                    return;
                case 4:
                    character.AddHistory("Assigned to difficult or backbreaking labour.");
                    if (dice.RollHigh(character.EnduranceDM, 8))
                    {
                        character.Parole += -1;
                        var skills = new SkillTemplateCollection();
                        skills.AddRange(CharacterBuilder.SpecialtiesFor("Athletics"));
                        skills.Add("Mechanic");
                        skills.Add("Melee", "Unarmed");
                        skills.RemoveOverlap(character.Skills, 1);
                        if (skills.Count > 0)
                            character.Skills.Add(dice.Choose(skills), 1);
                    }
                    else
                    {
                        character.Parole += 1;
                    }
                    return;
                case 5:
                    if (dice.RollHigh(character.Skills.BestSkillLevel("Persuade", "Melee"), 8))
                    {
                        character.AddHistory("Joined a prison gang");
                        character.LongTermBenefits.PrisonSurvivalDM += 1;
                        character.Parole += 1;

                        var skills = new SkillTemplateCollection();
                        skills.Add("Deception");
                        skills.Add("Persuade");
                        skills.Add("Stealth");
                        skills.Add("Melee", "Unarmed");
                        skills.RemoveOverlap(character.Skills, 1);
                        if (skills.Count > 0)
                            character.Skills.Add(dice.Choose(skills), 1);
                    }
                    else
                    {
                        character.AddHistory("Offended a prison gang you tried to join. Gain an Enemy.");
                    }
                    return;
                case 6:
                    character.AddHistory("Vocational Training.");
                    if (dice.RollHigh(character.EducationDM, 8))
                    {
                        character.Skills.Increase(dice.Choose(CharacterBuilder.RandomSkills), 1);
                    }
                    return;
                case 7:
                    switch (dice.D(6))
                    {
                        case 1:
                            character.AddHistory("A riot engulfs the prison.");
                            if (dice.D(6) <= 2)
                            {
                                CharacterBuilder.Injury(character, dice);
                            }
                            else
                            {
                                character.AddHistory("Loot the guards/other prisoners.");
                                character.BenefitRolls += 1;
                            }
                            return;
                        case 2:
                            character.AddHistory("Make friends with another inmate; gain them as a Contact.");
                            return;
                        case 3:
                            character.AddHistory("You gain a new Rival among the inmates or guards.");
                            return;
                        case 4:
                            var oldParole = character.Parole;
                            character.Parole = dice.D(6) + 4;

                            if (oldParole > character.Parole)
                                character.AddHistory("You are moved to a lower security prison.");
                            else if (oldParole == character.Parole)
                                character.AddHistory("You are moved to a different prison.");
                            else
                                character.AddHistory("You are moved to a higher security prison.");
                            return;
                        case 5:
                            character.AddHistory("Good Behaviour.");
                            character.Parole += -2;
                            return;
                        case 6:
                            character.AddHistory("Attacked by another prisoner.");
                            if (!dice.RollHigh(character.Skills.GetLevel("Melee", "Unarmed"), 8))
                            {
                                CharacterBuilder.Injury(character, dice);
                            }
                            return;
                    }
                    return;
                case 8:
                    character.AddHistory("Parole hearing.");
                    character.Parole += -1;
                    return;
                case 9:
                    character.AddHistory("Hire a new lawyer.");
                    var advocate = dice.D(6) - 2;
                    if (dice.RollHigh(advocate, 8))
                    {
                        character.Parole -= dice.D(6);
                    }
                    return;
                case 10:
                    character.AddHistory("Special Duty.");
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.Add("Admin");
                        skillList.Add("Advocate");
                        skillList.Add("Electronics", "Computers");
                        skillList.Add("Steward");
                        character.Skills.Increase(dice.Choose(skillList));
                    }
                    return;
                case 11:
                    character.AddHistory("The warden takes an interest in your case.");
                    character.Parole += -2;
                    return;
                case 12:
                    if (dice.RollHigh(8))
                    {
                        character.AddHistory("Saved a guard or prison officer. Gain an Ally.");
                        character.Parole += -2;
                    }
                    else
                    {
                        character.AddHistory("Attmped but failed to save a guard or prison officer.");
                        CharacterBuilder.Injury(character, dice, false);
                    }
                    return;
            }
        }

        internal void Mishap(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    CharacterBuilder.Injury(character, dice, true);
                    return;
                case 2:
                    character.AddHistory("Accused of assaulting a prison guard.");
                    character.Parole += 2;
                    return;
                case 3:
                    if (dice.D(2) == 1)
                    {
                        character.AddHistory("Persecuted by a prison gang.");
                        character.BenefitRolls = 0;
                    }
                    else
                    {
                        if (dice.RollHigh(character.Skills.GetLevel("Melee", "Unarmed"), 8))
                        {
                            character.AddHistory("Beaten by a prison gang.");
                            CharacterBuilder.Injury(character, dice, true);
                        }
                        else
                        {
                            character.AddHistory("Defeated a prison gang. Gain an Enemy.");
                            character.Parole += 1;
                        }
                    }
                    return;
                case 4:
                    character.AddHistory("A guard takes a dislike to you.");
                    character.Parole += 1;
                    return;
                case 5:
                    character.AddHistory("Disgraced. Word of your criminal past reaches your homeworld.");
                    character.SocialStanding += -1;
                    return;
                case 6:
                    CharacterBuilder.Injury(character, dice, false);
                    return;
            }
        }

        internal void UpdateTitle(Character character, CareerHistory careerHistory, Dice dice)
        {
            switch (careerHistory.Rank)
            {
                case 1:
                    return;
                case 2:
                    var skillList = new SkillTemplateCollection(CharacterBuilder.SpecialtiesFor("Athletics"));
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);

                    return;
                case 3:
                    return;
                case 4:
                    character.Skills.Add("Advocate");
                    return;
                case 5:
                    return;
                case 6:
                    character.Endurance += 1;
                    return;
            }
        }
        protected abstract void AssignmentSkills(Character character, Dice dice, int roll, bool level0);

        protected virtual void BasicTraining(Character character, Dice dice, bool firstCareer)
        {
            //Rank 0 skill
            character.Skills.Add("Melee", "Unarmed", 1);

            if (firstCareer)
                for (var i = 1; i < 7; i++)
                    ServiceSkill(character, dice, i, true);
            else
                ServiceSkill(character, dice, dice.D(6), true);

        }

        protected void PersonalDevelopment(Character character, Dice dice, int roll, bool level0)
        {
            switch (roll)
            {
                case 1:
                    character.Strength += 1;
                    return;
                case 2:
                    character.Skills.Increase("Melee", "Unarmed");
                    return;
                case 3:
                    character.Endurance += 1;
                    return;
                case 4:
                    character.Skills.Increase("Jack-of-All-Trades");
                    return;
                case 5:
                    character.Education += 1;
                    return;
                case 6:
                    character.Skills.Increase("Gambler");
                    return;
            }
        }

        protected void ServiceSkill(Character character, Dice dice, int roll, bool level0)
        {
            if (level0)
            {
                switch (roll)
                {
                    case 1:
                        character.Skills.Add(dice.Choose(CharacterBuilder.SpecialtiesFor("Athletics")));
                        return;
                    case 2:
                        character.Skills.Add("Deception");
                        return;
                    case 3:
                        character.Skills.Add(dice.Choose(CharacterBuilder.SpecialtiesFor("Profession")));
                        return;
                    case 4:
                        character.Skills.Add("Streetwise");
                        return;
                    case 5:
                        character.Skills.Add("Melee", "Unarmed");
                        return;
                    case 6:
                        character.Skills.Add("Persuade");
                        return;
                }
            }
            else
            {
                switch (roll)
                {
                    case 1:
                        character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Athletics")));
                        return;
                    case 2:
                        character.Skills.Increase("Deception");
                        return;
                    case 3:
                        character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Profession")));
                        return;
                    case 4:
                        character.Skills.Increase("Streetwise");
                        return;
                    case 5:
                        character.Skills.Increase("Melee", "Unarmed");
                        return;
                    case 6:
                        character.Skills.Increase("Persuade");
                        return;
                }
            }
        }
    }
}

