using Grauenwolf.TravellerTools.Characters.Careers.Humaniti;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers.Imperium;

public class HumanitiImperiumCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : SpeciesCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Third Imperium";
    public override ImmutableArray<Gender> Genders { get; } = ImmutableArray.Create<Gender>(new("F", "Female", 1), new("M", "Male", 1));
    public override string Species => "Humaniti";

    public override string SpeciesGroup => "Humaniti";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Humaniti";
    protected override bool AllowPsionics => true;

    protected override CareerLists CreateCareerList()
    {
        var draftCareers = new List<CareerBase>()
        {
            //Duplicates are needed to make the odds match the book.

            //    Navy
            new Navy_EngineerGunner(this),
            new Navy_Flight(this),
            new Navy_LineCrew(this),

            //    Army
            new Army_Support(this),
            new Army_Cavalry(this),
            new Army_Infantry(this),

            //    Marine
            new Marine_Support(this),
            new Marine_GroundAssault(this),
            new Marine_StarMarine(this),

            //    Merchant Marine
            new Merchant_MerchantMarine(this),
            new Merchant_MerchantMarine(this),
            new Merchant_MerchantMarine(this),

            //    Scout
            new Scout_Courier(this),
            new Scout_Explorer(this),
            new Scout_Surveyor(this),

            //    Law Enforcement
            new Agent_LawEnforcement(this),
            new Agent_LawEnforcement(this),
            new Agent_LawEnforcement(this),
        };

        var defaultCareers = new List<CareerBase>()
        {
            new Drifter_Barbarian(this),
            new Drifter_Scavenger(this),
            new Drifter_Wanderer(this),
        };

        var careers = new List<CareerBase>
        {
           //Pre-Career Options
            new Precareers.ArmyAcademy(this),
            new Precareers.ColonialUpbringing(this),
            new Precareers.MarineAcademy(this),
            new Precareers.MerchantAcademy(this),
            new Precareers.NavalAcademy(this),
            new Precareers.PsionicCommunity(this),
            new Precareers.SchoolOfHardKnocks(this),
            new Precareers.SpacerCommunity (this),
            new Precareers.University(this),

            //Careers
            new Agent_CorporateAgent(this),
            new Agent_Intelligence(this),
            new Agent_LawEnforcement(this),
            new Army_Cavalry(this),
            new Army_Infantry(this),
            new Army_Support(this),
            new Believer_HolyWarrior(this),
            new Believer_MainstreamBeliever(this),
            new Believer_Missionary(this),
            new Citizen_Colonist(this),
            new Citizen_Corporate(this),
            new Citizen_Worker(this),
            new Drifter_Barbarian(this),
            new Drifter_Scavenger(this),
            new Drifter_Wanderer(this),
            new Entertainer_Artist(this),
            new Entertainer_Journalist(this),
            new Entertainer_Performer(this),
            new Marine_GroundAssault(this),
            new Marine_StarMarine(this),
            new Marine_Support(this),
            new Merchant_Broker(this),
            new Merchant_FreeTrader(this),
            new Merchant_MerchantMarine(this),
            new Navy_EngineerGunner(this),
            new Navy_Flight(this),
            new Navy_LineCrew(this),
            new Noble_Administrator(this),
            new Noble_Dilettante(this),
            new Noble_Diplomat(this),
            new Prisoner_Fixer(this),
            new Prisoner_Inmate(this),
            new Prisoner_Thug(this),
            new Psion_Adept(this),
            new Psion_PsiWarrrior(this),
            new Psion_WildTalent(this),
            new Retired(this),
            new Rogue_Enforcer(this),
            new Rogue_Pirate(this),
            new Rogue_Thief(this),
            new Scholar_FieldResearcher(this),
            new Scholar_Physician(this),
            new Scholar_Scientist(this),
            new Scout_Courier(this),
            new Scout_Explorer(this),
            new Scout_Surveyor(this),
            new Truther(this),
        };

        return new(defaultCareers.ToImmutableArray(), draftCareers.ToImmutableArray(), careers.ToImmutableArray());
    }

    protected override int RollForPsi(Character character, Dice dice) => dice.D(2, 6) - character.CurrentTerm;
}
