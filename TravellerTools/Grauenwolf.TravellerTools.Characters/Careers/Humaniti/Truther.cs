namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

internal class Truther(SpeciesCharacterBuilder speciesCharacterBuilder) : RanklessCareer("Truther", null, speciesCharacterBuilder)
{
    protected override int AdvancedEductionMin => int.MaxValue;

    protected override string SurvivalAttribute => "Fol";

    protected override int SurvivalTarget => 4;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Profession")));
                return;

            case 2:
                var skills = new SkillTemplateCollection();
                skills.AddRange(SpecialtiesFor(character, "Science"));
                skills.Add("Medic");
                character.Skills.Increase(dice.Choose(skills));
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Science")));
                return;

            case 4:
                character.Skills.Increase("Investigate");
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Science")));
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Science")));
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
                    character.AddHistory($"A corporate or government body wants to use {character.Name}'s knowledge in a way {character.Name} find questionable. {character.Name} refuse.", dice);
                }
                else
                {
                    var enemies = dice.D(3);
                    character.AddHistory($"A corporate or government body wants to use {character.Name}'s knowledge in a way {character.Name} find questionable. {character.Name} agree, but gain {enemies} enemies..", dice);
                    character.AddEnemy(3);
                    character.BenefitRolls += 1;
                    character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Science")));
                }
                return;

            case 4:
                character.AddHistory($"{character.Name} is called upon as a consultant for a secret research project. Gain one Contact", dice);
                character.BenefitRollDMs.Add(1);
                character.AddContact();
                return;

            case 5:
                character.AddHistory($"Understanding some new revelation requires a crash course in a field {character.Name} lacked knowledge of.", dice);
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(SpecialtiesFor(character, "Science"));
                    skills.AddRange(SpecialtiesFor(character, "Electronics"));
                    skills.RemoveOverlap(character.Skills, 1);
                    if (skills.Count > 0)
                        character.Skills.Add(dice.Choose(skills), 1);
                }
                return;

            case 6:
                character.AddHistory($"{character.Name}'s search for Truth takes {character.Name} out onto the frontiers.", dice);
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(SpecialtiesFor(character, "Survival"));
                    skills.AddRange(SpecialtiesFor(character, "Recon"));
                    character.Skills.Increase(dice.Choose(skills));
                }
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                character.AddHistory($"{character.Name} make a series of high-profile appearances on entertainment/topical news shows.", dice);
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(SpecialtiesFor(character, "Carouse"));
                    skills.AddRange(SpecialtiesFor(character, "Persuade"));
                    character.Skills.Increase(dice.Choose(skills));
                    character.Following += 1;
                }
                return;

            case 9:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"A golden opportunity lands in {character.Name}'s lap but taking advantage of it will harm someone else’s career. {character.Name} decide not to and gain an Ally.", dice);
                    character.AddAlly();
                }
                else
                {
                    var enemies = dice.D(3);
                    character.AddHistory($"A golden opportunity lands in {character.Name}'s lap but taking advantage of it will harm someone else’s career. {character.Name} agree, but gain {enemies} enemies..", dice);
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
                            skills.AddRange(SpecialtiesFor(character, "Science"));
                            skills.AddRange(SpecialtiesFor(character, "Electronics"));
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
                    character.AddHistory($"{character.Name} come into contact with a mysterious group who are interested in {character.Name}'s Truth. Interactions with them are vague and secretive. Gain {contacts} Contacts.", dice);
                    character.AddContact(contacts);
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(SpecialtiesFor(character, "Streetwise"));
                    skills.AddRange(SpecialtiesFor(character, "Carouse"));
                    skills.AddRange(SpecialtiesFor(character, "Recon"));
                    character.Skills.Increase(dice.Choose(skills));
                    return;
                }
            case 11:
                character.AddHistory($"A very public disagreement with another truther or a disbeliever goes in {character.Name}'s favour and {character.Name} become something of a celebrity. Gain a Rival.", dice);
                character.AddRival();
                character.SocialStanding += 1;
                return;

            case 12:
                character.AddHistory($"{character.Name}'s previously obscure work becomes a lot more mainstream after a new scientific or \r\nacademic breakthrough.", dice);
                character.Following += dice.D(3);
                return;
        }
    }

    internal override void Mishap(Character character, Dice dice, int age)
    {
        switch (dice.D(6))
        {
            case 1:

                character.AddHistory($"Injured in a misadventure or attacked by a deranged objector to {character.Name}'s work.", age);
                Injury(character, dice, true, age);
                return;

            case 2:
                character.AddHistory($"Someone affected by the Truth {character.Name} seek or reveal bears a grudge and swears to kill {character.Name}. Gain an Enemy.", age);
                character.AddEnemy();
                return;

            case 3:
                character.AddHistory($"{character.Name} is discredited, rightly or wrongly.", age);
                character.SocialStanding -= 2;
                character.Following -= dice.D(3);
                character.NextTermBenefits.MusterOut = false;
                return;

            case 4:
                character.AddHistory($"{character.Name} discover that at least some of {character.Name}'s work is based on erroneous thinking.", age);

                var skills = character.Skills.Where(s => s.Name == "Science" && s.Level >= 1).ToList();
                dice.Choose(skills).Level -= 1;

                return;

            case 5:
                character.AddHistory($"{character.Name}'s Truth is misused or misrepresented and {character.Name} get the blame.", age);
                character.BenefitRolls = 0;
                character.NextTermBenefits.MusterOut = false;
                return;

            case 6:
                var rivals = dice.D(3);
                character.AddHistory($"{character.Name}'s Truthing alienates former colleagues and contacts, who are determined to bring {character.Name} down. Gain {rivals} Rivals.", age);
                character.AddRival(rivals);
                return;
        }
    }

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        return (character.Following == null && character.Following >= 0);
    }

    internal override void Run(Character character, Dice dice)
    {
        if (character.Following == null)
            character.Following = 0;

        base.Run(character, dice);
    }

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
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Language")));
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
