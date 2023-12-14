namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

internal class Truther(CharacterBuilder characterBuilder) : RanklessCareer("Truther", null, characterBuilder)
{
    protected override int AdvancedEductionMin => int.MaxValue;

    protected override string SurvivalAttribute => "Fol";

    protected override int SurvivalTarget => 4;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Profession")));
                return;

            case 2:
                var skills = new SkillTemplateCollection();
                skills.AddRange(SpecialtiesFor("Science"));
                skills.Add("Medic");
                character.Skills.Increase(dice.Choose(skills));
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                return;

            case 4:
                character.Skills.Increase("Investigate");
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                return;
        }
    }

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        var roll = dice.D(6);

        if (all || roll == 1)
            character.Skills.Add("Investigate");
        if (all || roll == 2)
            character.Skills.Add("Art");
        if (all || roll == 3)
            character.Skills.Add("Language");
        if (all || roll == 4)
            character.Skills.Add("Electronics");
        if (all || roll == 5)
            character.Skills.Add("Diplomat");
        if (all || roll == 6)
            character.Skills.Add("Persuade");
    }

    internal override void Event(Character character, Dice dice)
    {
        switch (dice.D(2, 6))
        {
            case 2:
                MishapRollAge(character, dice);
                character.NextTermBenefits.MusterOut = false;
                return;

            case 3:
                if (dice.NextBoolean())
                {
                    character.AddHistory("A corporate or government body wants to use your knowledge in a way you find questionable. You refuse.", dice);
                }
                else
                {
                    var enemies = dice.D(3);
                    character.AddHistory($"A corporate or government body wants to use your knowledge in a way you find questionable. You agree, but gain {enemies} enemies..", dice);
                    character.AddEnemy(3);
                    character.BenefitRolls += 1;
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                }
                return;

            case 4:
                character.AddHistory("You are called upon as a consultant for a secret research project. Gain one Contact", dice);
                character.BenefitRollDMs.Add(1);
                character.AddContact();
                return;

            case 5:
                character.AddHistory("Understanding some new revelation requires a crash course in a field you lacked knowledge of.", dice);
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(SpecialtiesFor("Science"));
                    skills.AddRange(SpecialtiesFor("Electronics"));
                    skills.RemoveOverlap(character.Skills, 1);
                    if (skills.Count > 0)
                        character.Skills.Add(dice.Choose(skills), 1);
                }
                return;

            case 6:
                character.AddHistory("Your search for Truth takes you out onto the frontiers.", dice);
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(SpecialtiesFor("Survival"));
                    skills.AddRange(SpecialtiesFor("Recon"));
                    character.Skills.Increase(dice.Choose(skills));
                }
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                character.AddHistory("You make a series of high-profile appearances on entertainment/topical news shows.", dice);
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(SpecialtiesFor("Carouse"));
                    skills.AddRange(SpecialtiesFor("Persuade"));
                    character.Skills.Increase(dice.Choose(skills));
                    character.Following += 1;
                }
                return;

            case 9:
                if (dice.NextBoolean())
                {
                    character.AddHistory("A golden opportunity lands in your lap but taking advantage of it will harm someone else’s career. You decide not to and gain an Ally.", dice);
                    character.AddAlly();
                }
                else
                {
                    var enemies = dice.D(3);
                    character.AddHistory($"A golden opportunity lands in your lap but taking advantage of it will harm someone else’s career. You agree, but gain {enemies} enemies..", dice);
                    character.AddEnemy(3);

                    switch (dice.D(3))
                    {
                        case 1:
                            character.Following += 2;

                            break;

                        case 2:
                            character.SocialStanding += 1;

                            break;

                        case 3:
                            var skills = new SkillTemplateCollection();
                            skills.AddRange(SpecialtiesFor("Science"));
                            skills.AddRange(SpecialtiesFor("Electronics"));
                            skills.Add("Medic");

                            if (skills.Count > 0)
                                character.Skills.Increase(dice.Choose(skills), 2);
                            break;
                    }
                }
                return;

            case 10:
                {
                    var contacts = dice.D(3);
                    character.AddHistory($"You come into contact with a mysterious group who are interested in your Truth. Interactions with them are vague and secretive. Gain {contacts} Contacts.", dice);
                    character.AddContact(contacts);
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(SpecialtiesFor("Streetwise"));
                    skills.AddRange(SpecialtiesFor("Carouse"));
                    skills.AddRange(SpecialtiesFor("Recon"));
                    character.Skills.Increase(dice.Choose(skills));
                    return;
                }
            case 11:
                character.AddHistory("A very public disagreement with another truther or a disbeliever goes in your favour and you become something of a celebrity. Gain a Rival.", dice);
                character.AddRival();
                character.SocialStanding += 1;
                return;

            case 12:
                character.AddHistory("Your previously obscure work becomes a lot more mainstream after a new scientific or \r\nacademic breakthrough.", dice);
                character.Following += dice.D(3);
                return;
        }
    }

    internal override void Mishap(Character character, Dice dice, int age)
    {
        switch (dice.D(6))
        {
            case 1:

                character.AddHistory("Injured in a misadventure or attacked by a deranged objector to your work.", age);
                Injury(character, dice, true, age);
                return;

            case 2:
                character.AddHistory("Someone affected by the Truth you seek or reveal bears a grudge and swears to kill you. Gain an Enemy. ", age);
                character.AddEnemy();
                return;

            case 3:
                character.AddHistory("You are discredited, rightly or wrongly.", age);
                character.SocialStanding -= 2;
                character.Following -= dice.D(3);
                character.NextTermBenefits.MusterOut = false;
                return;

            case 4:
                character.AddHistory("You discover that at least some of your work is based on erroneous thinking.", age);

                var skills = character.Skills.Where(s => s.Name == "Science" && s.Level >= 1).ToList();
                dice.Choose(skills).Level -= 1;

                return;

            case 5:
                character.AddHistory("Your Truth is misused or misrepresented and you get the blame.", age);
                character.BenefitRolls = 0;
                character.NextTermBenefits.MusterOut = false;
                return;

            case 6:
                var rivals = dice.D(3);
                character.AddHistory("Your Truthing alienates former colleagues and contacts, who are determined to bring you down. Gain {rivals} Rivals.", age);
                character.AddRival(rivals);
                return;
        }
    }

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck) => true;

    internal override void ServiceSkill(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Investigate");
                return;

            case 2:
                character.Skills.Increase("Art", "Writing");
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Language")));
                return;

            case 4:
                character.Skills.Increase("Electronics", "Computer");
                return;

            case 5:
                character.Skills.Increase("Diplomat");
                return;

            case 6:
                character.Skills.Increase("Persuade");
                return;
        }
    }

    protected override void AdvancedEducation(Character character, Dice dice) => throw new NotImplementedException();

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Intellect += 1;
                return;

            case 2:
                character.Education += 1;
                return;

            case 3:
                character.Following += 1;
                return;

            case 4:
                character.Skills.Increase("Admin");
                return;

            case 5:
                character.Skills.Increase("Carouse");
                return;

            case 6:
                character.Skills.Increase("Persuade");
                return;
        }
    }
}
