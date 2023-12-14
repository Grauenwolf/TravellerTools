using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers.Bwap;

public class BwapCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilderLocator characterBuilderLocator) : CharacterBuilder(dataPath, nameGenerator, characterBuilderLocator)
{
    public override string Species => "Bwap";

    protected override bool AllowPsionics => true;

    internal override void FixupSkills(Character character, Dice dice)
    {
        Fix("Admin");
        Fix("Advocate");
        Fix("Broker");

        void Fix(string name)
        {
            var skill = character.Skills[name];
            if (skill != null && skill.Level == 0)
                skill.Level = 1;
        }
    }

    protected override int AgingRollDM(Character character)
    {
        return -1 * character.CurrentTerm - 1;
    }

    protected override CareerLists CreateCareerList()
    {
        var draftCareers = new List<CareerBase>()
        {
            //Duplicates are needed to make the odds match the book.

            //    Navy
            new Humaniti.EngineerGunner(this),
            new Humaniti.Flight(this),
            new Humaniti.LineCrew(this),

            //    Army
            new ArmySupport(this),
            new Cavalry(this),
            new Infantry(this),

            //    Marine
            new MarineSupport(this),
            new GroundAssault(this),
            new StarMarine(this),

            //    Merchant Marine
            new MerchantMarine(this),
            new MerchantMarine(this),
            new MerchantMarine(this),

            //    Scout
            new Courier(this),
            new Explorer(this),
            new Surveyor(this),

            //    Law Enforcement
            new Humaniti.LawEnforcement(this),
            new Humaniti.LawEnforcement(this),
            new Humaniti.LawEnforcement(this),
        };

        var defaultCareers = new List<CareerBase>()
        {
            new Humaniti.Wanderer(this),
            new Humaniti.Scavenger(this),
            new Humaniti.Barbarian(this)
        };

        var careers = new List<CareerBase>
        {
           //Pre-Career Options
            new Precareers.ArmyAcademy(this),
            new Precareers.MarineAcademy(this),
            new Precareers.NavalAcademy(this),
            new Precareers.ColonialUpbringing(this),
            new Precareers.MerchantAcademy(this),
            new Precareers.PsionicCommunity(this),

            //Careers
            new Humaniti.Adept(this),
            new Humaniti.Administrator(this),
            new Humaniti.Artist(this),
            new Broker(this),
            new Humaniti.Colonist(this),
            new Corporate(this),
            new Humaniti.Dilettante(this),
            new Humaniti.Diplomat(this),
            new Humaniti.FieldResearcher(this),
            new Humaniti.Fixer(this),
            new FreeTrader(this),
            new Humaniti.Inmate(this),
            new Humaniti.Journalist(this),
            new Humaniti.Performer(this),
            new Humaniti.Physician(this),
            new Humaniti.PsiWarrrior(this),
            new Retired(this),
            new Humaniti.Scientist(this),
            new Humaniti.Thug(this),
            new Humaniti.WildTalent(this),
            new Humaniti.Worker(this),
            new Humaniti.MainstreamBeliever(this),
            new Humaniti.Missionary(this),
            new Humaniti.HolyWarrior(this),

            //These are not implemented yet
            //new Truther(this),
        };

        //Need to remove duplicates that affect odds
        careers.AddRange(defaultCareers.Distinct());
        careers.AddRange(draftCareers.Distinct());

        return new(defaultCareers.ToImmutableArray(), draftCareers.ToImmutableArray(), careers.ToImmutableArray());
    }

    protected override void InitialCharacterStats(Dice dice, Character character)
    {
        base.InitialCharacterStats(dice, character);
        character.Strength -= 4;
        character.Endurance -= 4;

        if (character.Strength < 1)
            character.Strength = 1;
        if (character.Endurance < 1)
            character.Endurance = 1;
    }

    protected override int RollForPsi(Character character, Dice dice) => dice.D(2, 6) - 3 - character.CurrentTerm;
}
