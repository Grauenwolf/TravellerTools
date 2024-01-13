using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

public class ImperiumDolphinCharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilderLocator characterBuilderLocator) : CharacterBuilder(dataPath, nameGenerator, characterBuilderLocator)
{
    public override ImmutableArray<Gender> Genders { get; } = ImmutableArray.Create<Gender>(new("F", "Female", 1), new("M", "Male", 1));
    public override string Species => "Imperium Dolphin";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Dolphin";
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

                var templates = new SkillTemplateCollection(Book(character).RandomSkills);
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
            new DolphinMilitary_Guardian(this),
            new DolphinMilitary_UnderwaterCommando(this),
            new DolphinMilitary_SeaPatrol(this),
        };

        var defaultCareers = new List<CareerBase>()
        {
            new DolphinCivilian_Nomad(this),
        };

        var careers = new List<CareerBase>
        {
           //Pre-Career Options
            new Precareers.ArmyAcademy(this),
            new Precareers.MarineAcademy(this),
            new Precareers.NavalAcademy(this),
            new University(this),
            new DolphinMilitaryAcademy(this),

            //Careers
            new Agent_CorporateAgent(this),
            new Agent_Intelligence(this),
            new Believer_HolyWarrior(this),
            new Believer_MainstreamBeliever(this),
            new Believer_Missionary(this),
            new Citizen_Colonist(this),
            new Citizen_Corporate(this),
            new Citizen_Worker(this),
            new DolphinCivilian_HistorianPoet(this),
            new DolphinCivilian_Liaison(this),
            new DolphinCivilian_Nomad(this),
            new DolphinMilitary_Guardian(this),
            new DolphinMilitary_Guardian(this),
            new DolphinMilitary_SeaPatrol(this),
            new DolphinMilitary_SeaPatrol(this),
            new DolphinMilitary_UnderwaterCommando(this),
            new DolphinMilitary_UnderwaterCommando(this),
            new Entertainer_Artist(this),
            new Entertainer_Journalist(this),
            new Entertainer_Performer(this),
            new Prisoner_Fixer(this),
            new Prisoner_Inmate(this),
            new Prisoner_Thug(this),
            new Humaniti.Psion_Adept(this),
            new Humaniti.Psion_PsiWarrrior(this),
            new Retired(this),
            new Rogue_Enforcer(this),
            new Rogue_Pirate(this),
            new Rogue_Thief(this),
            new Scholar_FieldResearcher(this),
            new Scholar_Physician(this),
            new Scholar_Scientist(this),
            new Truther(this),
        };

        return new(defaultCareers.ToImmutableArray(), draftCareers.ToImmutableArray(), careers.ToImmutableArray());
    }

    protected override void InitialCharacterStats(Dice dice, Character character)
    {
        base.InitialCharacterStats(dice, character);

        character.Age = 12 + dice.D(3) - dice.D(3);
        character.Strength += 4;
        character.Endurance += 2;
        character.SocialStanding -= 4;

        if (character.SocialStanding < 1)
            character.SocialStanding = 1;
    }

    protected override int RollForPsi(Character character, Dice dice) => dice.D(2, 6) - character.CurrentTerm;
}
