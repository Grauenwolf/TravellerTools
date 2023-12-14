using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

public class ImperiumDolphinCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilderLocator characterBuilderLocator) : CharacterBuilder(dataPath, nameGenerator, characterBuilderLocator)
{
    public override string Species => "Imperium Dolphin";

    protected override int AgingRollMinAge => 20;
    protected override bool AllowPsionics => true;

    protected override string CharacterBuilderFilename => "CharacterBuilder.Dolphin.xml";

    internal override void FixupSkills(Character character, Dice dice)
    {
        foreach (var skill in character.Skills.ToList())
        {
            if (skill.Name == "Broker" || skill.Name == "Gambler")
            {
                character.Skills.Remove(skill);

                var templates = new SkillTemplateCollection(Book.RandomSkills);
                templates.RemoveOverlap(character.Skills, skill.Level);
                character.Skills.Add(dice.Choose(templates), skill.Level);
            }
        }
    }

    protected override CareerLists CreateCareerList()
    {
        var draftCareers = new List<CareerBase>()
        {
            // Dolphin Military
            new SeaPatrol(this),
            new UnderwaterCommando(this),
            new Guardian(this),
        };

        var defaultCareers = new List<CareerBase>()
        {
            new Nomad(this),
        };

        var careers = new List<CareerBase>
        {
           //Pre-Career Options
            new Precareers.ArmyAcademy(this),
            new Precareers.MarineAcademy(this),
            new Precareers.NavalAcademy(this),
            new University(this),
            new DolphinMilitaryAcademy(this),

            //new Precareers.ColonialUpbringing(this),
            //new Precareers.MerchantAcademy(this),
            //new Precareers.PsionicCommunity(this),
            //new Precareers.SchoolOfHardKnocks(this),
            //new Precareers.SpacerCommunity (this),

            //Careers
            new Liaison(this),
            new HistorianPoet(this),
            new SeaPatrol(this),
            new UnderwaterCommando(this),
            new Guardian(this),
            new Humaniti.Adept(this),
            new Artist(this),
            new Colonist(this),
            new Corporate(this),
            new CorporateAgent(this),
            new Enforcer(this),
            new FieldResearcher(this),
            new Humaniti.Fixer(this),
            new Humaniti.Inmate(this),
            new Intelligence(this),
            new Journalist(this),
            new Performer(this),
            new Physician(this),
            new Pirate(this),
            new Humaniti.PsiWarrrior(this),
            new Retired(this),
            new Scientist(this),
            new Thief(this),
            new Humaniti.Thug(this),
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

    protected override void InitialCharacterStats(Dice dice, Character character)
    {
        base.InitialCharacterStats(dice, character);
        character.Age = 12;
        character.Strength += 4;
        character.Endurance += 2;
        character.SocialStanding -= 4;

        if (character.SocialStanding < 1)
            character.SocialStanding = 1;
    }

    protected override int RollForPsi(Character character, Dice dice) => dice.D(2, 6) - character.CurrentTerm;
}
