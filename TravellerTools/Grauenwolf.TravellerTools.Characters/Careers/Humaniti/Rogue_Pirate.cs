﻿namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Rogue_Pirate(CharacterBuilder characterBuilder) : Rogue("Pirate", characterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Dex";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Pilot")));
                return;

            case 2:
                character.Skills.Increase("Astrogation");
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Gunner")));
                return;

            case 4:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Engineer")));
                return;

            case 5:
                character.Skills.Increase("Vacc Suit");
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Melee")));
                return;
        }
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Lackey";
                return;

            case 1:
                careerHistory.Title = "Henchman";
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.AddRange(SpecialtiesFor("Pilot"));
                    skillList.AddRange(SpecialtiesFor("Gunner"));
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                return;

            case 2:
                careerHistory.Title = "Corporal";
                return;

            case 3:
                careerHistory.Title = "Sergeant";
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.AddRange(SpecialtiesFor("Gun Combat"));
                    skillList.AddRange(SpecialtiesFor("Melee"));
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }

                return;

            case 4:
                careerHistory.Title = "Lieutenant";
                return;

            case 5:
                careerHistory.Title = "Leader";
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.AddRange(SpecialtiesFor("Engineer"));
                    skillList.Add("Navigation");
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                return;

            case 6:
                careerHistory.Title = "Captain";
                return;
        }
    }
}
