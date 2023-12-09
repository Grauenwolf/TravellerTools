using Grauenwolf.TravellerTools.Characters.Careers;
using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters;

public class BwapCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilderLocator characterBuilderLocator) : CharacterBuilder(dataPath, nameGenerator, characterBuilderLocator)
{
    public override string Species => "Bwap";

    internal override void FixupSkills(Character character)
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
        return (-1 * character.CurrentTerm) - 1;
    }

    protected override ImmutableArray<CareerBase> CreateCareerList()
    {
        var careers = new List<CareerBase>
        {
            new Adept(this),
            new Administrator(this),
            new ArmySupport(this),
            new Artist(this),
            new Barbarian(this),
            new Broker(this),
            new Cavalry(this),
            new Colonist(this),
            new Corporate(this),
            new Courier(this),
            new Dilettante(this),
            new Diplomat(this),
            new EngineerGunner(this),
            new Explorer(this),
            new FieldResearcher(this),
            new Fixer(this),
            new Flight(this),
            new FreeTrader(this),
            new GroundAssault(this),
            new Infantry(this),
            new Inmate(this),
            new Journalist(this),
            new LawEnforcement(this),
            new LineCrew(this),
            new MarineSupport(this),
            new MerchantMarine(this),
            new Performer(this),
            new Physician(this),
            new PsiWarrrior(this),
            new Retired(this),
            new Scavenger(this),
            new Scientist(this),
            new StarMarine(this),
            new Surveyor(this),
            new Thug(this),
            new Wanderer(this),
            new WildTalent(this),
            new Worker(this),

            //These are not implemented yet
            //new MainstreamBeliever(this),
            //new Missionary(this),
            //new HolyWarrior(this),
            //new Truther(this),
        };
        return careers.ToImmutableArray();
    }

    protected override void InitialCharacterStats(Dice dice, Character character)
    {
        base.InitialCharacterStats(dice, character);
        character.Strength -= 4;
        character.Endurance -= 4;

        if (character.Strength < -0)
            character.Strength = 1;
        if (character.Endurance < -0)
            character.Endurance = 1;
    }
}
