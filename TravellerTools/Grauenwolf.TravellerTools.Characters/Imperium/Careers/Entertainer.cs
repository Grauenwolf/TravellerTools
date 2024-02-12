namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

abstract class Entertainer(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : NormalCareer("Entertainer", assignment, speciesCharacterBuilder)
{
    public override CareerGroup CareerGroup => CareerGroup.ImperiumCareer;
    public override CareerType CareerTypes => CareerType.ArtistOrPerformer;
    public override string? Source => "Traveller Core, page 30";
    protected override int AdvancedEductionMin => 10;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Art", "Carouse", "Drive", "Deception", "Persuade", "Steward");
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

                if (dice.RollHigh(character.Skills.BestSkillLevel("Investigate", "Art"), 8))
                {
                    character.AddHistory($"Invited to take part in a controversial event or exhibition that improves {character.Name}'s social standing.", dice);
                    character.SocialStanding += 1;
                }
                else
                {
                    character.AddHistory($"Invited to take part in a controversial event or exhibition that injures {character.Name}'s social standing.", dice);
                    character.SocialStanding += -1;
                }
                return;

            case 4:

                {
                    var age = character.AddHistory($"Join homeworld’s celebrity circles", dice);

                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Carouse");
                    skillList.Add("Persuade");
                    skillList.Add("Steward");
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count == 0 || dice.NextBoolean())
                    {
                        character.AddHistory($"Gain a contact", age);
                        character.AddContact();
                    }
                    else
                        character.Skills.Add(dice.Choose(skillList), 1);
                }

                return;

            case 5:
                character.AddHistory($"Works is especially well received and popular, making {character.Name} a minor celebrity", dice);
                character.BenefitRollDMs.Add(1);
                return;

            case 6:
                character.AddHistory($"Gain a patron in the arts. Gain an Ally", dice);
                character.AddAlly();
                character.CurrentTermBenefits.AdvancementDM += 2;
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:

                if (dice.NextBoolean())
                {
                    character.AddHistory($"Refused to criticise a questionable political leader on {character.Name}'s homeworld.", dice);
                }
                else
                {
                    var age = character.AddHistory($"Criticised a questionable political leader on {character.Name}'s homeworld, causing his downfall. Gain an Enemy", dice);
                    character.AddEnemy();
                    dice.Choose(character.Skills).Level += 1;
                    if (!dice.RollHigh(character.Skills.BestSkillLevel("Art", "Persuade"), 8))
                        Mishap(character, dice, age);
                }
                return;

            case 9:
                int count = dice.D(3);
                character.AddHistory($"Go on a tour of the sector, visiting several worlds. Gain {count} Contacts.", dice);
                character.AddContact(count);
                return;

            case 10:
                character.AddHistory($"One of {character.Name}'s pieces of art is stolen, and the investigation brings {character.Name} into the criminal underworld.", dice);
                AddOneSkill(character, dice, "Streetwise", "Investigate", "Recon", "Stealth");
                return;

            case 11:
                UnusualLifeEvent(character, dice);
                return;

            case 12:
                character.AddHistory($"Win a prestigious prize.", dice);
                character.CurrentTermBenefits.AdvancementDM += 100;
                return;
        }
    }

    internal override decimal MedicalPaymentPercentage(Character character, Dice dice)
    {
        var roll = dice.D(2, 6) + (character.LastCareer?.Rank ?? 0);
        if (roll >= 12)
            return 1.0M;
        if (roll >= 8)
            return 0.75M;
        if (roll >= 4)
            return 0.50M;
        return 0;
    }

    internal override void Mishap(Character character, Dice dice, int age)
    {
        switch (dice.D(6))
        {
            case 1:
                SevereInjury(character, dice, age);
                return;

            case 2:
                character.AddHistory($"Expose or are involved in a scandal of some sort.", age);
                return;

            case 3:
                character.AddHistory($"Public opinion turns on {character.Name}.", age);
                character.SocialStanding += -1;
                return;

            case 4:
                character.AddHistory($"{character.Name} is betrayed by a peer. One Ally or Contact becomes a Rival or Enemy", age);
                return;

            case 5:
                character.AddHistory($"An investigation, tour, project or expedition goes wrong, stranding {character.Name} far from home.", age);
                AddOneSkill(character, dice, "Survival", "Pilot", "Persuade", "Streetwise");
                return;

            case 6:
                character.AddHistory($"{character.Name} is forced out because of censorship or controversy. What truth did {character.Name} get too close to?", age);
                character.NextTermBenefits.QualificationDM += 2;
                return;
        }
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        Increase(character, dice, "Art", "Carouse", "Deception", "Drive", "Persuade", "Steward");
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Advocate", "Broker", "Deception", "Science", "Streetwise", "Diplomat");
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = Math.Max(character.IntellectDM, character.DexterityDM);
        dm += -1 * character.CareerHistory.Count;

        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 5, isPrecheck);
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        Increase(character, dice, "Dexterity", "Intellect", "SocialStanding", "Language", "Carouse", "Jack-of-All-Trades");
    }
}
