using Grauenwolf.TravellerTools.Characters.Careers.Humaniti;
using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers.Bwap;

public class BwapCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : SpeciesCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Third Imperium";
    public override ImmutableArray<Gender> Genders { get; } = ImmutableArray.Create<Gender>(new("F", "Female", 1), new("M", "Male", 1));
    public override string? Source => "Aliens of Charted Space Vol. 3, page 253";
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
            new Navy_EngineerGunner(this),
            new Navy_Flight(this),
            new Navy_LineCrew(this),

            //    Army
            new Army_Support(this){QualifyDM=-1},
            new Army_Cavalry(this){QualifyDM=-1},
            new Army_Infantry(this){QualifyDM=-1 },

            //    Marine
            new Marine_Support(this){QualifyDM=-3 },
            new Marine_GroundAssault(this){QualifyDM=-3 },
            new Marine_StarMarine(this){QualifyDM=-3 },

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

            //Careers
            new Agent_LawEnforcement(this),
            new Army_Cavalry(this){QualifyDM=-1 },
            new Army_Support(this){QualifyDM=-1 },
            new Believer_HolyWarrior(this),
            new Believer_MainstreamBeliever(this),
            new Believer_Missionary(this),
            new Citizen_Colonist(this),
            new Citizen_Corporate(this),
            new Citizen_Worker(this),
            new Drifter_Barbarian(this)  ,
            new Drifter_Scavenger(this),
            new Drifter_Wanderer(this),
            new Entertainer_Artist(this),
            new Entertainer_Journalist(this),
            new Entertainer_Performer(this),
            new Army_Infantry(this){QualifyDM=-1 },
            new Marine_GroundAssault(this){QualifyDM=-3 },
            new Marine_StarMarine(this){QualifyDM=-3 },
            new Marine_Support(this){QualifyDM=-3 },
            new Merchant_Broker(this){QualifyDM=2 },
            new Merchant_FreeTrader(this){QualifyDM=2 },
            new Merchant_MerchantMarine(this){QualifyDM=2 },
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
            new Scholar_FieldResearcher(this),
            new Scholar_Physician(this),
            new Scholar_Scientist(this),
            new Scout_Courier(this){QualifyDM=1 },
            new Scout_Explorer(this)   {QualifyDM=1 },
            new Scout_Surveyor(this){QualifyDM=1 },
            new Truther(this),
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

    class Citizen_Corporate(SpeciesCharacterBuilder speciesCharacterBuilder) : Humaniti.Citizen_Corporate(speciesCharacterBuilder)
    {
        protected override bool OnQualify(Character character, Dice dice, bool isPrecheck) => true;
    }
}
