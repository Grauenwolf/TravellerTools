//using Grauenwolf.TravellerTools.Characters.Careers.Humaniti;
using Grauenwolf.TravellerTools.Characters.Careers.Humaniti;
using Grauenwolf.TravellerTools.Characters.Careers.Imperium;
using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers.Dynchia;

public class DynchiaImperiumCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : HumanitiImperiumCharacterBuilder(dataPath, characterBuilder)
{
    public override string? Remarks => "Warrior race\r\nDigestive mutation\r\nPolydactyly";
    public override string? Source => "Journal of the Traveller’s Aid Society Vol 1, page 52";
    public override string Species => "Dynchia, Imperium";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Dynchia";

    protected override CareerLists CreateCareerList()
    {
        var draftCareers = new List<CareerBase>()
        {
            //Duplicates are needed to make the odds match the book.

            //    Navy
            new Navy_EngineerGunner(this){ CommissionDM= -2, AdvancementDM = -2},
            new Navy_Flight(this){ CommissionDM= -2, AdvancementDM = -2},
            new Navy_LineCrew(this){ CommissionDM= -2, AdvancementDM = -2},

            //    Army
            new Army_Support(this){ CommissionDM= -2, AdvancementDM = -2},
            new Army_Cavalry(this){ CommissionDM= -2, AdvancementDM = -2},
            new Army_Infantry(this){ CommissionDM= -2, AdvancementDM = -2},

            //    Marine
            new Marine_Support(this){ CommissionDM= -2, AdvancementDM = -2},
            new Marine_GroundAssault(this){ CommissionDM= -2, AdvancementDM = -2},
            new Marine_StarMarine(this){ CommissionDM= -2, AdvancementDM = -2},

            //    Merchant Marine
            new Merchant_MerchantMarine(this){  AdvancementDM = -2},
            new Merchant_MerchantMarine(this){  AdvancementDM = -2},
            new Merchant_MerchantMarine(this){  AdvancementDM = -2},

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
            new Army_Cavalry(this){ CommissionDM= -2, AdvancementDM = -2},
            new Army_Infantry(this){ CommissionDM= -2, AdvancementDM = -2},
            new Army_Support(this){ CommissionDM= -2, AdvancementDM = -2},
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
            new Marine_GroundAssault(this){ CommissionDM= -2, AdvancementDM = -2},
            new Marine_StarMarine(this){ CommissionDM= -2, AdvancementDM = -2},
            new Marine_Support(this){ CommissionDM= -2, AdvancementDM = -2},
            new Merchant_Broker(this),
            new Merchant_FreeTrader(this),
            new Merchant_MerchantMarine(this){ AdvancementDM = -2},
            new Navy_EngineerGunner(this){ CommissionDM= -2, AdvancementDM = -2},
            new Navy_Flight(this){ CommissionDM= -2, AdvancementDM = -2},
            new Navy_LineCrew(this){ CommissionDM= -2, AdvancementDM = -2},
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

    protected override void InitialCharacterStats(Dice dice, Character character)
    {
        base.InitialCharacterStats(dice, character);
        character.Strength = dice.D(1, 6) + 3;
        character.Dexterity += 1;
        character.Education += 1;
    }
}
