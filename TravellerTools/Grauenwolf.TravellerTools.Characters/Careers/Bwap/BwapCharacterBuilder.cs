using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers.Bwap;

public class BwapCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilder characterBuilder) : SpeciesCharacterBuilder(dataPath, nameGenerator, characterBuilder)
{
    public override ImmutableArray<Gender> Genders { get; } = ImmutableArray.Create<Gender>(new("F", "Female", 1), new("M", "Male", 1));
    public override string Species => "Bwap";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Bwap";
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

    protected override int AgingRollDM(int currentTerm) => (-1 * currentTerm) - 1;

    protected override CareerLists CreateCareerList()
    {
        var draftCareers = new List<CareerBase>()
        {
            //Duplicates are needed to make the odds match the book.

            //    Navy
            new Humaniti.Navy_EngineerGunner(this),
            new Humaniti.Navy_Flight(this),
            new Humaniti.Navy_LineCrew(this),

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
            new Precareers.ArmyAcademy(this),
            new Precareers.ColonialUpbringing(this),
            new Precareers.MarineAcademy(this),
            new Precareers.MerchantAcademy(this),
            new Precareers.NavalAcademy(this),
            new Precareers.PsionicCommunity(this),

            //Careers
            new Humaniti.Agent_LawEnforcement(this),
            new Army_Cavalry(this),
            new Army_Support(this),
            new Humaniti.Believer_HolyWarrior(this),
            new Humaniti.Believer_MainstreamBeliever(this),
            new Humaniti.Believer_Missionary(this),
            new Humaniti.Citizen_Colonist(this),
            new Citizen_Corporate(this),
            new Humaniti.Citizen_Worker(this),
            new Humaniti.Drifter_Barbarian(this)  ,
            new Humaniti.Drifter_Scavenger(this),
            new Humaniti.Drifter_Wanderer(this),
            new Humaniti.Entertainer_Artist(this),
            new Humaniti.Entertainer_Journalist(this),
            new Humaniti.Entertainer_Performer(this),
            new Army_Infantry(this),
            new Marine_GroundAssault(this),
            new Marine_StarMarine(this),
            new Marine_Support(this),
            new Merchant_Broker(this),
            new Merchant_FreeTrader(this),
            new Merchant_MerchantMarine(this),
            new Humaniti.Navy_EngineerGunner(this),
            new Humaniti.Navy_Flight(this),
            new Humaniti.Navy_LineCrew(this),
            new Humaniti.Noble_Administrator(this),
            new Humaniti.Noble_Dilettante(this),
            new Humaniti.Noble_Diplomat(this),
            new Prisoner_Fixer(this),
            new Prisoner_Inmate(this),
            new Prisoner_Thug(this),
            new Humaniti.Psion_Adept(this),
            new Humaniti.Psion_PsiWarrrior(this),
            new Humaniti.Psion_WildTalent(this),
            new Retired(this),
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
        character.Strength -= 4;
        character.Endurance -= 4;

        if (character.Strength < 1)
            character.Strength = 1;
        if (character.Endurance < 1)
            character.Endurance = 1;
    }

    protected override int RollForPsi(Character character, Dice dice) => dice.D(2, 6) - 3 - character.CurrentTerm;
}
