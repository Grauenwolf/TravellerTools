using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers.Bwap;

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
        return -1 * character.CurrentTerm - 1;
    }

    protected override ImmutableArray<CareerBase> CreateCareerList()
    {
        var careers = new List<CareerBase>
        {
            new Humaniti.Adept(this),
            new Humaniti.Administrator(this),
            new ArmySupport(this),
            new Humaniti.Artist(this),
            new Humaniti.Barbarian(this),
            new Broker(this),
            new Cavalry(this),
            new Humaniti.Colonist(this),
            new Corporate(this),
            new Courier(this),
            new Humaniti.Dilettante(this),
            new Humaniti.Diplomat(this),
            new Humaniti.EngineerGunner(this),
            new Explorer(this),
            new Humaniti.FieldResearcher(this),
            new Humaniti.Fixer(this),
            new Humaniti.Flight(this),
            new FreeTrader(this),
            new GroundAssault(this),
            new Infantry(this),
            new Humaniti.Inmate(this),
            new Humaniti.Journalist(this),
            new Humaniti.LawEnforcement(this),
            new Humaniti.LineCrew(this),
            new MarineSupport(this),
            new MerchantMarine(this),
            new Humaniti.Performer(this),
            new Humaniti.Physician(this),
            new Humaniti.PsiWarrrior(this),
            new Retired(this),
            new Humaniti.Scavenger(this),
            new Humaniti.Scientist(this),
            new StarMarine(this),
            new Surveyor(this),
            new Humaniti.Thug(this),
            new Humaniti.Wanderer(this),
            new Humaniti.WildTalent(this),
            new Humaniti.Worker(this),

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

        if (character.Strength < 1)
            character.Strength = 1;
        if (character.Endurance < 1)
            character.Endurance = 1;
    }
}
