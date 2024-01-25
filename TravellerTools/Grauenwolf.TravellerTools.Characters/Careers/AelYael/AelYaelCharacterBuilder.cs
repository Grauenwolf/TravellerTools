using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers.AelYael;

public class AelYaelCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : SpeciesCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override ImmutableArray<Gender> Genders { get; } = ImmutableArray.Create<Gender>(new("F", "Female", 1), new("M", "Male", 1));
    public override string Species => "Ael Yael";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Ael_Yael";
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
            //new Humaniti.Merchant_MerchantMarine(this),
            //new Humaniti.Merchant_MerchantMarine(this),
            //new Humaniti.Merchant_MerchantMarine(this),

            //    Scout
            new Scout_Courier(this),
            new Scout_Explorer(this),
            new Scout_Surveyor(this),

            //    Law Enforcement
            new Humaniti.Agent_LawEnforcement(this),
            new Humaniti.Agent_LawEnforcement(this),
            new Humaniti.Agent_LawEnforcement(this),
        };

        var defaultCareers = new List<CareerBase>()
        {
            new Humaniti.Drifter_Barbarian(this),
            new Humaniti.Drifter_Scavenger(this),
            new Humaniti.Drifter_Wanderer(this),
        };

        var careers = new List<CareerBase>
        {
           //Pre-Career Options
            //new Precareers.ArmyAcademy(this),
            new Precareers.ColonialUpbringing(this),
            //new Precareers.MarineAcademy(this),
            //new Precareers.MerchantAcademy(this),
            //new Precareers.NavalAcademy(this),
            new Precareers.PsionicCommunity(this),
            new Precareers.SchoolOfHardKnocks(this),
            new Precareers.SpacerCommunity (this),
            new Precareers.University(this),

            //Careers
            new Humaniti.Agent_CorporateAgent(this),
            new Humaniti.Agent_Intelligence(this),
            new Humaniti.Agent_LawEnforcement(this),
            new Army_Cavalry(this),
            new Army_Infantry(this),
            new Army_Support(this),
            new Humaniti.Believer_HolyWarrior(this),
            new Humaniti.Believer_MainstreamBeliever(this),
            new Humaniti.Believer_Missionary(this),
            new Humaniti.Citizen_Colonist(this),
            new Humaniti.Citizen_Corporate(this),
            new Humaniti.Citizen_Worker(this),
            new Humaniti.Drifter_Barbarian(this),
            new Humaniti.Drifter_Scavenger(this),
            new Humaniti.Drifter_Wanderer(this),
            new Humaniti.Entertainer_Artist(this),
            new Humaniti.Entertainer_Journalist(this),
            new Humaniti.Entertainer_Performer(this),
            new Marine_GroundAssault(this),
            new Marine_StarMarine(this),
            new Marine_Support(this),
            new Humaniti.Merchant_Broker(this),
            new Humaniti.Merchant_FreeTrader(this),
            //new Humaniti.Merchant_MerchantMarine(this),
            new Navy_EngineerGunner(this),
            new Navy_Flight(this),
            new Navy_LineCrew(this),
            //new Humaniti.Noble_Administrator(this),
            //new Humaniti.Noble_Dilettante(this),
            //new Humaniti.Noble_Diplomat(this),
            new Prisoner_Fixer(this),
            new Prisoner_Inmate(this),
            new Prisoner_Thug(this),
            new Humaniti.Psion_Adept(this),
            new Humaniti.Psion_PsiWarrrior(this),
            new Humaniti.Psion_WildTalent(this),
            new Retired(this),
            new Humaniti.Rogue_Enforcer(this),
            new Humaniti.Rogue_Pirate(this),
            new Humaniti.Rogue_Thief(this),
            new Humaniti.Scholar_FieldResearcher(this),
            new Humaniti.Scholar_Physician(this),
            new Humaniti.Scholar_Scientist(this),
            new Scout_Courier(this),
            new Scout_Explorer(this),
            new Scout_Surveyor(this),
            new Humaniti.Truther(this),
        };

        return new(defaultCareers.ToImmutableArray(), draftCareers.ToImmutableArray(), careers.ToImmutableArray());
    }

    protected override void InitialCharacterStats(Dice dice, Character character)
    {
        base.InitialCharacterStats(dice, character);
        character.Strength += -1;
    }

    protected override int RollForPsi(Character character, Dice dice) => dice.D(2, 6) - character.CurrentTerm;
}
