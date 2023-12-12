using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

public class TezcatCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilderLocator characterBuilderLocator) : CharacterBuilder(dataPath, nameGenerator, characterBuilderLocator)
{
    static readonly ImmutableList<string> s_MandatoryBackgroundSkills = ImmutableList.Create("Gun Combat", "Melee", "Stealth");
    static readonly ImmutableList<string> s_OptionalBackgroundSkills = ImmutableList.Create("Admin", "Animals", "Art", "Athletics", "Carouse", "Drive", "Science", "Seafarer", "Streetwise", "Survival", "Vacc Suit", "Electronics", "Flyer", "Language", "Mechanic", "Medic", "Profession");
    public override string Species => "Tezcat";

    protected override bool AllowPsionics => false;
    protected override string CharacterBuilderFilename => "CharacterBuilder.Tezcat.xml";

    protected override void AddBackgroundSkills(Dice dice, Character character)
    {
        var skillPicks = character.EducationDM + 3;
        var requiredPicks = dice.Choose(s_MandatoryBackgroundSkills, Math.Min(3, skillPicks), allowDuplicates: false);
        foreach (var skill in requiredPicks)
            character.Skills.Add(skill); //all skills added at level 0

        skillPicks -= requiredPicks.Count;
        if (skillPicks > 0)
        {
            var backgroundSKills = dice.Choose(s_OptionalBackgroundSkills, skillPicks, allowDuplicates: false);
            foreach (var skill in backgroundSKills)
                character.Skills.Add(skill); //all skills added at level 0
        }
    }

    protected override int AgingRollDM(Character character)
    {
        return -1 * character.CurrentTerm - 1;
    }

    protected override CareerLists CreateCareerList()
    {
        var defaultCareers = new List<CareerBase>()
        {
            new Humaniti.Wanderer(this),
            new Humaniti.Scavenger(this),
            new Humaniti.Barbarian(this),
        };

        var draftCareers = new List<CareerBase>()
        {
            //Duplicates are needed to make the odds match the book.

            //Soul Hunter
            new Commando(this),
            new Flight(this),
            new Support(this),

            //    Navy
            new Humaniti.EngineerGunner(this),
            new Humaniti.Flight(this),
            new Humaniti.LineCrew(this),

            //    Army
            new Humaniti.ArmySupport(this),
            new Humaniti.Cavalry(this),
            new Humaniti.Infantry(this),

            //    Marine
            new Humaniti.MarineSupport(this),
            new Humaniti.GroundAssault(this),
            new Humaniti.StarMarine(this),

            //    Merchant Marine
            new Humaniti.MerchantMarine(this),
            new Humaniti.MerchantMarine(this),
            new Humaniti.MerchantMarine(this),

            //    Scout
            new Humaniti.Courier(this),
            new Humaniti.Explorer(this),
            new Humaniti.Surveyor(this),

            //    Law Enforcement
            new Humaniti.LawEnforcement(this),
            new Humaniti.LawEnforcement(this),
            new Humaniti.LawEnforcement(this),
        };

        var careers = new List<CareerBase>
        {
           //Pre-Career Options
            new Precareers.ArmyAcademy(this),
            new Precareers.MarineAcademy(this),
            new Precareers.NavalAcademy(this),
            new Precareers.University(this),

            //Careers
            new Humaniti.Adept(this),
            new Humaniti.Artist(this),
            new Humaniti.Broker(this),
            new Humaniti.Colonist(this),
            new Humaniti.Corporate(this),
            new Humaniti.CorporateAgent(this),
            new Humaniti.Enforcer(this),
            new Humaniti.FieldResearcher(this),
            new Humaniti.Fixer(this),
            new Humaniti.FreeTrader(this),
            new Humaniti.Inmate(this),
            new Humaniti.Intelligence(this),
            new Humaniti.Journalist(this),
            new Humaniti.Performer(this),
            new Humaniti.Physician(this),
            new Humaniti.Pirate(this),
            new Humaniti.PsiWarrrior(this),
            new Retired(this),
            new Humaniti.Scientist(this),
            new Humaniti.Thief(this),
            new Humaniti.Thug(this),
            new Humaniti.WildTalent(this),
            new Humaniti.Worker(this),
            new Commando(this),
            new Flight(this),
            new Support(this),
            new Academic(this),
            new Ecclesiastic(this),
            new Partisan(this),
        };

        //Need to remove duplicates that affect odds
        careers.AddRange(defaultCareers.Distinct());
        careers.AddRange(draftCareers.Distinct());

        return new(defaultCareers.ToImmutableArray(), draftCareers.ToImmutableArray(), careers.ToImmutableArray());
    }

    protected override void ForceFirstTerm(Character character, Dice dice)
    {
        //Most will enter the draft when they become adults.
        if (dice.D(6) <= 4)
            character.NextTermBenefits.MustEnroll = RollDraft(dice).ShortName;
    }

    protected override void InitialCharacterStats(Dice dice, Character character)
    {
        base.InitialCharacterStats(dice, character);
        character.Dexterity += 1;
        character.Endurance -= 1;

        if (character.Endurance < 1)
            character.Endurance = 1;
    }

    protected override int RollForPsi(Character character, Dice dice) => 0;
}
