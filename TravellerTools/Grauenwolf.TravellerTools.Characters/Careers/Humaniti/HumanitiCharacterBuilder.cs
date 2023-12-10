using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

public class HumanitiCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilderLocator characterBuilderLocator) : CharacterBuilder(dataPath, nameGenerator, characterBuilderLocator)
{
    public override string Species => "Humaniti";

    protected override ImmutableArray<CareerBase> CreateCareerList()
    {
        var careers = new List<CareerBase>
        {
           //Pre-Career Options
            new ArmyAcademy(this),
            new MarineAcademy(this),
            new NavalAcademy(this),
            new University(this),
            new ColonialUpbringing(this),
            new MerchantAcademy(this),

            //These are not implemented yet
            //new PsionicCommunity(this),
            //new SchoolOfHardKnocks(this),
            //new SpacerCommunity (this),

            //Careers
            new Adept(this),
            new Administrator(this),
            new ArmySupport(this),
            new Artist(this),
            new Barbarian(this),
            new Broker(this),
            new Cavalry(this),
            new Colonist(this),
            new Corporate(this),
            new CorporateAgent(this),
            new Courier(this),
            new Dilettante(this),
            new Diplomat(this),
            new Enforcer(this),
            new EngineerGunner(this),
            new Explorer(this),
            new FieldResearcher(this),
            new Fixer(this),
            new Flight(this),
            new FreeTrader(this),
            new GroundAssault(this),
            new Infantry(this),
            new Inmate(this),
            new Intelligence(this),
            new Journalist(this),
            new LawEnforcement(this),
            new LineCrew(this),
            new MarineSupport(this),
            new MerchantMarine(this),
            new Performer(this),
            new Physician(this),
            new Pirate(this),
            new PsiWarrrior(this),
            new Retired(this),
            new Scavenger(this),
            new Scientist(this),
            new StarMarine(this),
            new Surveyor(this),
            new Thief(this),
            new Thug(this),
            new Wanderer(this),
            new WildTalent(this),
            new Worker(this),
            new MainstreamBeliever(this),
            new Missionary(this),
            new HolyWarrior(this),

            //These are not implemented yet
            //new Truther(this),
        };
        return careers.ToImmutableArray();
    }
}
