using Grauenwolf.TravellerTools.Characters.Careers.Humaniti;
using Grauenwolf.TravellerTools.Characters.Careers.Precareers;
using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

public class DolphinImperiumCharacterBuilder(string dataPath, CharacterBuilder characterBuilder) : SpeciesCharacterBuilder(dataPath, characterBuilder)
{
    public override string Faction => "Third Imperium";
    public override ImmutableArray<Gender> Genders { get; } = ImmutableArray.Create<Gender>(new("F", "Female", 1), new("M", "Male", 1));
    public override string? Source => "Aliens of Charted Space Vol. 3, page 168";
    public override string Species => "Dolphin, Imperium";
    public override string SpeciesUrl => "https://wiki.travellerrpg.com/Dolphin";
    public override int StartingAge => 12;
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
            new ArmyAcademy(this),
            new MarineAcademy(this),
            new NavalAcademy(this),
            new University(this){QualifyDM=-1},
            new DolphinMilitaryAcademy(this),

            //Careers
            new Agent_CorporateAgent(this){RequiredSkill="Vacc Suit"},
            new Agent_Intelligence(this){RequiredSkill="Vacc Suit"},
            new Agent_LawEnforcement(this){RequiredSkill="Vacc Suit"},
            new Army_Cavalry(this) {RequiredSkill="Vacc Suit"},
            new Army_Infantry(this) {RequiredSkill="Vacc Suit"},
            new Army_Support(this)  {RequiredSkill="Vacc Suit"},
            new Believer_HolyWarrior(this){RequiredSkill="Vacc Suit"},
            new Believer_MainstreamBeliever(this){RequiredSkill="Vacc Suit"},
            new Believer_Missionary(this){RequiredSkill="Vacc Suit"},
            new Citizen_Colonist(this){RequiredSkill="Vacc Suit"},
            new Citizen_Corporate(this){RequiredSkill="Vacc Suit"},
            new Citizen_Worker(this){RequiredSkill="Vacc Suit"},
            new DolphinCivilian_HistorianPoet(this),
            new DolphinCivilian_Liaison(this),
            new DolphinCivilian_Nomad(this),
            new DolphinMilitary_Guardian(this),
            new DolphinMilitary_SeaPatrol(this),
            new DolphinMilitary_UnderwaterCommando(this),
            new Entertainer_Artist(this){RequiredSkill="Vacc Suit"},
            new Entertainer_Journalist(this){RequiredSkill="Vacc Suit"},
            new Entertainer_Performer(this){RequiredSkill="Vacc Suit"},
            new Marine_GroundAssault(this){RequiredSkill="Vacc Suit"},
            new Marine_StarMarine(this){RequiredSkill="Vacc Suit"},
            new Marine_Support(this){RequiredSkill="Vacc Suit"},
            new Navy_EngineerGunner(this){RequiredSkill="Vacc Suit"},
            new Navy_Flight(this){RequiredSkill="Vacc Suit"},
            new Navy_LineCrew(this){RequiredSkill="Vacc Suit"},
            new Prisoner_Fixer(this),
            new Prisoner_Inmate(this),
            new Prisoner_Thug(this),
            new Psion_Adept(this),
            new Psion_PsiWarrrior(this),
            new Retired(this),
            new Rogue_Enforcer(this){RequiredSkill="Vacc Suit"},
            new Rogue_Pirate(this){RequiredSkill="Vacc Suit"},
            new Rogue_Thief(this){RequiredSkill="Vacc Suit"},
            new Scholar_FieldResearcher(this){RequiredSkill="Vacc Suit", QualifyDM=1},
            new Scholar_Physician(this){RequiredSkill="Vacc Suit", QualifyDM=1},
            new Scholar_Scientist(this){RequiredSkill="Vacc Suit", QualifyDM=1},
            new Scout_Surveyor(this){RequiredSkill="Vacc Suit", QualifyDM=1},
            new Scout_Explorer(this){RequiredSkill="Vacc Suit", QualifyDM=1},
            new Scout_Courier(this){RequiredSkill="Vacc Suit", QualifyDM=1},
            new Truther(this){RequiredSkill="Vacc Suit"},
        };

        return new(defaultCareers.ToImmutableArray(), draftCareers.ToImmutableArray(), careers.ToImmutableArray());
    }

    protected override void InitialCharacterStats(Dice dice, Character character)
    {
        base.InitialCharacterStats(dice, character);

        character.Age = StartingAge;
        character.Strength += 4;
        character.Endurance += 2;
        character.SocialStanding -= 4;

        if (character.SocialStanding < 1)
            character.SocialStanding = 1;
    }

    protected override int RollForPsi(Character character, Dice dice) => dice.D(2, 6) - character.CurrentTerm;
}
