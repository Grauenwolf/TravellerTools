using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

public class TezcatCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : SpeciesCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    static readonly ImmutableList<string> s_MandatoryBackgroundSkills = ImmutableList.Create("Gun Combat", "Melee", "Stealth");
    static readonly ImmutableList<string> s_OptionalBackgroundSkills = ImmutableList.Create("Admin", "Animals", "Art", "Athletics", "Carouse", "Drive", "Science", "Seafarer", "Streetwise", "Survival", "Vacc Suit", "Electronics", "Flyer", "Language", "Mechanic", "Medic", "Profession");
    public override string Faction => "Tezcat";
    public override ImmutableArray<Gender> Genders { get; } = ImmutableArray.Create<Gender>(new("F", "Female", 1), new("M", "Male", 1));
    public override string? Source => "Aliens of Charted Space Vol. 4, page 224";
    public override string Species => "Tezcat";

    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Tezcat";
    protected override bool AllowPsionics => false;
    protected override string CharacterBuilderFilename => "CharacterBuilder.Tezcat.xml";

    protected override void AddBackgroundSkills(Dice dice, Character character)
    {
        var skillPicks = character.EducationDM + 3;
        if (character.MaxAge < StartingAge)
            skillPicks = (int)Math.Ceiling(skillPicks / 2.0);

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

    protected override int AgingRollDM(int currentTerm) => -1 * currentTerm - 1;

    protected override CareerLists CreateCareerList()
    {
        var defaultCareers = new List<CareerBase>()
        {
            new Humaniti.Drifter_Barbarian(this),
            new Humaniti.Drifter_Scavenger(this),
            new Humaniti.Drifter_Wanderer(this),
        };

        var draftCareers = new List<CareerBase>()
        {
            //Duplicates are needed to make the odds match the book.

            //Soul Hunter
            new Soulhunter_Commando(this),
            new Soulhunter_Flight(this),
            new Soulhunter_Support(this),

            //    Navy
            new Humaniti.Navy_EngineerGunner(this),
            new Humaniti.Navy_Flight(this),
            new Humaniti.Navy_LineCrew(this),

            //    Army
            new Humaniti.Army_Support(this),
            new Humaniti.Army_Cavalry(this),
            new Humaniti.Army_Infantry(this),

            //    Marine
            new Humaniti.Marine_Support(this),
            new Humaniti.Marine_GroundAssault(this),
            new Humaniti.Marine_StarMarine(this),

            //    Merchant Marine
            new Humaniti.Merchant_MerchantMarine(this),
            new Humaniti.Merchant_MerchantMarine(this),
            new Humaniti.Merchant_MerchantMarine(this),

            //    Scout
            new Humaniti.Scout_Courier(this),
            new Humaniti.Scout_Explorer(this),
            new Humaniti.Scout_Surveyor(this),

            //    Law Enforcement
            new Humaniti.Agent_LawEnforcement(this),
            new Humaniti.Agent_LawEnforcement(this),
            new Humaniti.Agent_LawEnforcement(this),
        };

        var careers = new List<CareerBase>
        {
           //Pre-Career Options
            new Precareers.ArmyAcademy(this),
            new Precareers.MarineAcademy(this),
            new Precareers.NavalAcademy(this),
            new Precareers.University(this),
            new Precareers.MerchantAcademy(this),

            //Careers
            new Humaniti.Agent_CorporateAgent(this),
            new Humaniti.Agent_Intelligence(this),
            new Humaniti.Agent_LawEnforcement(this),
            new Humaniti.Army_Cavalry(this),
            new Humaniti.Army_Infantry(this),
            new Humaniti.Army_Support(this),
            new Humaniti.Citizen_Colonist(this),
            new Humaniti.Citizen_Corporate(this),
            new Humaniti.Citizen_Worker(this),
            new Humaniti.Drifter_Barbarian(this),
            new Humaniti.Drifter_Scavenger(this),
            new Humaniti.Drifter_Wanderer(this),
            new Humaniti.Entertainer_Artist(this),
            new Humaniti.Entertainer_Journalist(this),
            new Humaniti.Entertainer_Performer(this),
            new Humaniti.Marine_GroundAssault(this),
            new Humaniti.Marine_StarMarine(this),
            new Humaniti.Marine_Support(this),
            new Humaniti.Merchant_Broker(this),
            new Humaniti.Merchant_FreeTrader(this),
            new Humaniti.Merchant_MerchantMarine(this),
            new Humaniti.Navy_EngineerGunner(this),
            new Humaniti.Navy_Flight(this),
            new Humaniti.Navy_LineCrew(this),
            new Prisoner_Fixer(this),
            new Prisoner_Inmate(this),
            new Prisoner_Thug(this),
            new Retired(this),
            new Humaniti.Rogue_Enforcer(this),
            new Humaniti.Rogue_Pirate(this),
            new Humaniti.Rogue_Thief(this),
            new Humaniti.Scholar_FieldResearcher(this),
            new Humaniti.Scholar_Physician(this),
            new Humaniti.Scholar_Scientist(this),
            new Humaniti.Scout_Courier(this),
            new Humaniti.Scout_Explorer(this),
            new Humaniti.Scout_Surveyor(this),
            new ShaperPriest_Academic(this),
            new ShaperPriest_Ecclesiastic(this),
            new ShaperPriest_Partisan(this),
            new Soulhunter_Commando(this),
            new Soulhunter_Flight(this),
            new Soulhunter_Support(this),
        };

        return new(defaultCareers.ToImmutableArray(), draftCareers.ToImmutableArray(), careers.ToImmutableArray());
    }

    protected override void ForceFirstTerm(Character character, Dice dice)
    {
        //Most will enter the draft when they become adults.
        if (dice.D(6) <= 4)
            character.NextTermBenefits.MustEnroll = RollDraft(character, dice).ShortName;
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
