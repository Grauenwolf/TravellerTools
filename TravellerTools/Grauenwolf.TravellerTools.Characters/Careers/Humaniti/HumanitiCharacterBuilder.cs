using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

public class HumanitiCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilderLocator characterBuilderLocator) : CharacterBuilder(dataPath, nameGenerator, characterBuilderLocator)
{
    public override string Species => "Humaniti";

    protected override bool AllowPsionics => true;

    protected override CareerLists CreateCareerList()
    {
        var draftCareers = new List<CareerBase>()
        {
            //Duplicates are needed to make the odds match the book.

            //    Navy
            new EngineerGunner(this),
            new Flight(this),
            new LineCrew(this),

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
            new LawEnforcement(this),
            new LawEnforcement(this),
            new LawEnforcement(this),
        };

        var defaultCareers = new List<CareerBase>()
        {
            new Wanderer(this),
            new Scavenger(this),
            new Barbarian(this)
        };

        var careers = new List<CareerBase>
        {
           //Pre-Career Options
            new Precareers.ArmyAcademy(this),
            new Precareers.MarineAcademy(this),
            new Precareers.NavalAcademy(this),
            new Precareers.University(this),
            new Precareers.ColonialUpbringing(this),
            new Precareers.MerchantAcademy(this),
            new Precareers.PsionicCommunity(this),
            new Precareers.SchoolOfHardKnocks(this),
            new Precareers.SpacerCommunity (this),

            //Careers
            new Adept(this),
            new Administrator(this),
            new Artist(this),
            new Broker(this),
            new Colonist(this),
            new Corporate(this),
            new CorporateAgent(this),
            new Dilettante(this),
            new Diplomat(this),
            new Enforcer(this),
            new FieldResearcher(this),
            new Fixer(this),
            new FreeTrader(this),
            new Inmate(this),
            new Intelligence(this),
            new Journalist(this),
            new Performer(this),
            new Physician(this),
            new Pirate(this),
            new PsiWarrrior(this),
            new Retired(this),
            new Scientist(this),
            new Thief(this),
            new Thug(this),
            new WildTalent(this),
            new Worker(this),
            new MainstreamBeliever(this),
            new Missionary(this),
            new HolyWarrior(this),

            //These are not implemented yet
            //new Truther(this),
        };

        //Need to remove duplicates that affect odds
        careers.AddRange(defaultCareers.Distinct());
        careers.AddRange(draftCareers.Distinct());

        return new(defaultCareers.ToImmutableArray(), draftCareers.ToImmutableArray(), careers.ToImmutableArray());
    }

    protected override int RollForPsi(Character character, Dice dice) => dice.D(2, 6) - character.CurrentTerm;
}
